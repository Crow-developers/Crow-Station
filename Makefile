# Crow Station developer commands. Run `make` or `make help` to list them.
# Requirements: .NET 10 SDK, Node.js 22, Docker with Compose v2, GNU Make.

SHELL := /usr/bin/env bash
.SHELLFLAGS := -eu -o pipefail -c
.DEFAULT_GOAL := help

BACKEND  := backend
SOLUTION := $(BACKEND)/CrowStation.slnx
API      := $(BACKEND)/src/CrowStation.Api
WEB      := frontend/web
COMPOSE  := docker compose
VERSION  ?= 0.0.0-local

##@ General

.PHONY: help
help: ## Show this help
	@awk 'BEGIN {FS = ":.*##"; printf "\nUsage: make \033[36m<target>\033[0m\n"} \
		/^[a-zA-Z0-9_-]+:.*?##/ { printf "  \033[36m%-20s\033[0m %s\n", $$1, $$2 } \
		/^##@/ { printf "\n\033[1m%s\033[0m\n", substr($$0, 5) }' $(MAKEFILE_LIST)

.PHONY: setup
setup: .env hooks ## First-time setup: .env, git hooks, .NET tools, restore and npm install
	cd $(BACKEND) && dotnet tool restore
	dotnet restore $(SOLUTION)
	npm --prefix $(WEB) ci

.PHONY: hooks
hooks: ## Install the repository git hooks (format check, commit message check)
	git config core.hooksPath .githooks
	chmod +x .githooks/*

.env:
	cp .env.example .env

##@ Local stack (Docker Compose)

.PHONY: infra
infra: .env ## Start PostgreSQL, Redis and storage only
	$(COMPOSE) up -d --wait postgres redis storage

.PHONY: up
up: .env ## Build and start the whole stack (api on :8080, web on :3000)
	$(COMPOSE) up -d --build --wait

.PHONY: down
down: ## Stop the stack (keeps data)
	$(COMPOSE) down

.PHONY: reset
reset: ## Stop the stack and delete all local data volumes
	$(COMPOSE) down -v --remove-orphans

.PHONY: logs
logs: ## Follow logs of every service
	$(COMPOSE) logs -f

.PHONY: ps
ps: ## Show service status
	$(COMPOSE) ps

##@ Backend (.NET)

.PHONY: api
api: ## Run the API locally with hot reload (needs `make infra`)
	dotnet watch --project $(API) run

.PHONY: backend-build
backend-build: ## Build the solution (warnings are errors)
	dotnet build $(SOLUTION) --configuration Release

.PHONY: backend-test
backend-test: ## Run unit, architecture and integration tests (integration needs Docker)
	dotnet test --solution $(SOLUTION) --configuration Release

.PHONY: backend-test-unit
backend-test-unit: ## Run tests that do not need Docker (unit + architecture)
	for project in $(BACKEND)/tests/*.UnitTests $(BACKEND)/tests/*.Architecture.Tests; do \
		dotnet test --project $$project --configuration Release; \
	done

.PHONY: backend-format
backend-format: ## Apply code style fixes
	dotnet format $(SOLUTION)

.PHONY: backend-lint
backend-lint: ## Verify formatting and analyzers without changing files
	dotnet format $(SOLUTION) --verify-no-changes

.PHONY: migration
migration: ## Add an EF Core migration: make migration NAME=AddCourses
	@test -n "$(NAME)" || (echo "Usage: make migration NAME=<MigrationName>" && exit 1)
	cd $(BACKEND) && dotnet tool run dotnet-ef migrations add $(NAME) --project src/CrowStation.Infrastructure --startup-project src/CrowStation.Api --output-dir Persistence/Migrations

.PHONY: migrate
migrate: ## Apply EF Core migrations to the local database
	cd $(BACKEND) && dotnet tool run dotnet-ef database update --project src/CrowStation.Infrastructure --startup-project src/CrowStation.Api

##@ Frontend (Next.js)

.PHONY: web
web: ## Run the web app in dev mode on :3000
	API_BASE_URL=http://localhost:8080 npm --prefix $(WEB) run dev

.PHONY: web-lint
web-lint: ## ESLint, TypeScript and Prettier checks
	npm --prefix $(WEB) run lint
	npm --prefix $(WEB) run typecheck
	npm --prefix $(WEB) run format:check

.PHONY: web-format
web-format: ## Apply Prettier formatting
	npm --prefix $(WEB) run format

.PHONY: web-test
web-test: ## Run frontend unit tests
	npm --prefix $(WEB) test

.PHONY: web-build
web-build: ## Production build of the web app
	npm --prefix $(WEB) run build

##@ Quality gates

.PHONY: lint
lint: backend-lint web-lint ## All linters

.PHONY: test
test: backend-test web-test ## All tests

.PHONY: build
build: backend-build web-build ## All builds

.PHONY: ci
ci: lint build test docker-build ## Run what CI runs, locally

##@ Docker images

.PHONY: docker-build
docker-build: ## Build production images for api and web
	docker build --build-arg VERSION=$(VERSION) -t crow-station-api:$(VERSION) $(BACKEND)
	docker build -t crow-station-web:$(VERSION) $(WEB)

.PHONY: clean
clean: ## Remove build outputs
	dotnet clean $(SOLUTION) >/dev/null
	rm -rf $(WEB)/.next $(WEB)/coverage $(WEB)/test-results
	find $(BACKEND) -type d \( -name TestResults -o -name bin -o -name obj \) -prune -exec rm -rf {} +
