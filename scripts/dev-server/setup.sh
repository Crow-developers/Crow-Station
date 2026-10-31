#!/usr/bin/env bash
# Crow Station: dev server setup for Debian 13 (trixie).
#
# Installs Docker, generates a private .env with random secrets, builds and starts the stack,
# then checks the API and prints the Scalar URL.
#
# Run from a clone of the repository:
#   sudo ./scripts/dev-server/setup.sh               # bind API/web to the Tailscale IP if present
#   sudo ./scripts/dev-server/setup.sh --bind 127.0.0.1
#   sudo ./scripts/dev-server/setup.sh --no-start    # prepare only
#
# Safe to run again: it never overwrites an existing .env and skips what is already installed.
set -Eeuo pipefail

REPO_DIR=$(cd "$(dirname "${BASH_SOURCE[0]}")/../.." && pwd)
ENV_FILE="$REPO_DIR/.env"
OPERATOR=${SUDO_USER:-}
PUBLIC_BIND=""
START=1

log()  { printf '\033[1;34m==>\033[0m %s\n' "$*"; }
warn() { printf '\033[1;33mWARN:\033[0m %s\n' "$*" >&2; }
die()  { printf '\033[1;31mERROR:\033[0m %s\n' "$*" >&2; exit 1; }
trap 'die "failed at line $LINENO: $BASH_COMMAND"' ERR

usage() {
  sed -n '2,13p' "${BASH_SOURCE[0]}" | sed 's/^# \{0,1\}//'
  exit 0
}

while [[ $# -gt 0 ]]; do
  case "$1" in
    --bind)     [[ $# -ge 2 ]] || die "--bind needs an address"; PUBLIC_BIND=$2; shift 2 ;;
    --no-start) START=0; shift ;;
    -h|--help)  usage ;;
    *)          die "unknown option: $1 (see --help)" ;;
  esac
done

# ---------------------------------------------------------------------------
check_system() {
  [[ $EUID -eq 0 ]] || die "run with sudo: sudo $0"
  [[ -f "$REPO_DIR/compose.yaml" && -f "$REPO_DIR/compose.server.yaml" ]] \
    || die "compose files not found in $REPO_DIR; run this script from the repository"
  # shellcheck source=/dev/null
  . /etc/os-release
  if [[ ${ID:-} != debian ]]; then
    die "this script supports Debian only (found: ${PRETTY_NAME:-unknown})"
  fi
  [[ ${VERSION_ID:-} == 13 ]] || warn "tested on Debian 13; found ${PRETTY_NAME:-unknown}"
  [[ -n $OPERATOR && $OPERATOR != root ]] \
    || warn "run with sudo from your normal user, so that user can be added to the docker group"
}

install_packages() {
  log "Installing base packages"
  apt-get update -qq
  DEBIAN_FRONTEND=noninteractive apt-get install -y -qq \
    ca-certificates curl git make jq openssl >/dev/null
}

install_docker() {
  if command -v docker >/dev/null && docker compose version >/dev/null 2>&1; then
    log "Docker already installed: $(docker --version)"
  else
    log "Installing Docker Engine from the official Docker repository"
    install -m 0755 -d /etc/apt/keyrings
    curl -fsSL https://download.docker.com/linux/debian/gpg -o /etc/apt/keyrings/docker.asc
    chmod a+r /etc/apt/keyrings/docker.asc
    # shellcheck source=/dev/null
    codename=$(. /etc/os-release && echo "$VERSION_CODENAME")
    cat > /etc/apt/sources.list.d/docker.sources <<SRC
Types: deb
URIs: https://download.docker.com/linux/debian
Suites: $codename
Components: stable
Signed-By: /etc/apt/keyrings/docker.asc
SRC
    apt-get update -qq
    DEBIAN_FRONTEND=noninteractive apt-get install -y -qq docker-ce docker-ce-cli containerd.io \
      docker-buildx-plugin docker-compose-plugin >/dev/null
  fi
  systemctl enable --now docker >/dev/null

  if [[ ! -f /etc/docker/daemon.json ]]; then
    log "Configuring Docker log rotation"
    cat > /etc/docker/daemon.json <<'JSON'
{
  "log-driver": "json-file",
  "log-opts": { "max-size": "10m", "max-file": "3" }
}
JSON
    systemctl restart docker
  fi
}

setup_operator() {
  [[ -n $OPERATOR && $OPERATOR != root ]] || return 0
  if id -nG "$OPERATOR" | tr ' ' '\n' | grep -qx docker; then
    log "User $OPERATOR is already in the docker group"
  else
    log "Adding $OPERATOR to the docker group (log out and in again to use docker without sudo)"
    usermod -aG docker "$OPERATOR"
  fi
}

detect_bind() {
  if [[ -n $PUBLIC_BIND ]]; then
    return
  fi
  if command -v tailscale >/dev/null && PUBLIC_BIND=$(tailscale ip -4 2>/dev/null | head -n1) && [[ -n $PUBLIC_BIND ]]; then
    log "Tailscale found: API and web will listen on $PUBLIC_BIND"
  else
    PUBLIC_BIND=127.0.0.1
    warn "Tailscale not found: API and web listen on 127.0.0.1 only (use an SSH tunnel, see docs/DEV-SERVER.md)"
  fi
}

allow_nonlocal_bind() {
  # A VPN address may not exist yet when Docker starts at boot. This lets Docker bind to it anyway,
  # so containers with restart: unless-stopped come back after a reboot.
  [[ $PUBLIC_BIND == 127.0.0.1 || $PUBLIC_BIND == 0.0.0.0 ]] && return 0
  log "Allowing Docker to bind $PUBLIC_BIND before the VPN is up"
  echo 'net.ipv4.ip_nonlocal_bind = 1' > /etc/sysctl.d/90-crow-station.conf
  sysctl -q -p /etc/sysctl.d/90-crow-station.conf
}

random_secret() { openssl rand -hex 24; }

write_env() {
  if [[ -f $ENV_FILE ]]; then
    log "Keeping the existing .env (delete it and run 'make reset' to start fresh)"
    PUBLIC_BIND=$(grep -E '^PUBLIC_BIND_ADDRESS=' "$ENV_FILE" | cut -d= -f2- || true)
    PUBLIC_BIND=${PUBLIC_BIND:-127.0.0.1}
    return
  fi
  [[ $PUBLIC_BIND == 0.0.0.0 ]] && warn "0.0.0.0 exposes the API to every network; Docker bypasses ufw for published ports"

  local origin_host=$PUBLIC_BIND
  [[ $origin_host == 127.0.0.1 ]] && origin_host=localhost

  log "Generating .env with random secrets"
  umask 077
  cat > "$ENV_FILE" <<ENV
# Generated by scripts/dev-server/setup.sh on $(date -u +%Y-%m-%dT%H:%M:%SZ). Do not commit.
# Changing POSTGRES_PASSWORD later has no effect on an existing database volume.
COMPOSE_FILE=compose.yaml:compose.server.yaml

POSTGRES_DB=crow_station
POSTGRES_USER=crow_app
POSTGRES_PASSWORD=$(random_secret)
POSTGRES_PORT=5432

REDIS_PORT=6379

STORAGE_ACCESS_KEY=crow-$(openssl rand -hex 4)
STORAGE_SECRET_KEY=$(random_secret)
STORAGE_BUCKET=crow-station
STORAGE_PORT=9000
STORAGE_CONSOLE_PORT=9001

API_PORT=8080
WEB_PORT=3000
WEB_ORIGIN=http://$origin_host:3000

BIND_ADDRESS=127.0.0.1
PUBLIC_BIND_ADDRESS=$PUBLIC_BIND
ENV
  if [[ -n $OPERATOR && $OPERATOR != root ]]; then
    chown "$OPERATOR:" "$ENV_FILE"
  fi
  chmod 600 "$ENV_FILE"
}

start_stack() {
  log "Building and starting the stack (the first build takes a few minutes)"
  (cd "$REPO_DIR" && docker compose up -d --build --wait --wait-timeout 600)
}

# ---------------------------------------------------------------------------
check_system
install_packages
install_docker
setup_operator
detect_bind
write_env
allow_nonlocal_bind

if [[ $START -eq 1 ]]; then
  start_stack
  "$REPO_DIR/scripts/dev-server/check.sh"
else
  log "Prepared. Start later with: cd $REPO_DIR && docker compose up -d --build --wait"
fi
