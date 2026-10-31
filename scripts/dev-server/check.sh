#!/usr/bin/env bash
# Crow Station: check the running stack and print the URLs.
# Usage: ./scripts/dev-server/check.sh   (from anywhere; reads the repository .env)
set -Eeuo pipefail

REPO_DIR=$(cd "$(dirname "${BASH_SOURCE[0]}")/../.." && pwd)
ENV_FILE="$REPO_DIR/.env"

value() { # value KEY DEFAULT
  local v=""
  [[ -r $ENV_FILE ]] && v=$(grep -E "^$1=" "$ENV_FILE" | tail -n1 | cut -d= -f2- || true)
  printf '%s' "${v:-$2}"
}

host=$(value PUBLIC_BIND_ADDRESS 127.0.0.1)
[[ $host == 0.0.0.0 ]] && host=127.0.0.1
api="http://$host:$(value API_PORT 8080)"
web="http://$host:$(value WEB_PORT 3000)"
failed=0

probe() { # probe NAME URL PATTERN
  if curl -fsSL --max-time 10 "$2" 2>/dev/null | grep -q "$3"; then
    printf '  \033[32mOK\033[0m    %-22s %s\n' "$1" "$2"
  else
    printf '  \033[31mFAIL\033[0m  %-22s %s\n' "$1" "$2"
    failed=1
  fi
}

echo "Services:"
(cd "$REPO_DIR" && docker compose ps --format '  {{.Service}}\t{{.State}}\t{{.Health}}') || true
echo
echo "Checks:"
probe "API live"            "$api/health/live"           "Healthy"
probe "API ready (DB, ...)" "$api/health/ready"          "Healthy"
probe "API v1 sample"       "$api/api/v1/diagnostics/time" "utcNow"
probe "OpenAPI document"    "$api/openapi/v1.json"       "openapi"
probe "Scalar UI"           "$api/scalar/"               "Scalar API Reference"
probe "Web"                 "$web/api/health"            "Healthy"
echo
echo "Open in a browser:"
echo "  Scalar (API docs):  $api/scalar/"
echo "  Web:                $web"
echo "  Storage console:    http://127.0.0.1:$(value STORAGE_CONSOLE_PORT 9001)  (server only; use an SSH tunnel)"
exit "$failed"
