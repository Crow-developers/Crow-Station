# syntax=docker/dockerfile:1.7

# ---- build ----------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Restore first, using only the files that affect restore, so this layer is cached across code changes.
COPY global.json Directory.Build.props Directory.Packages.props .editorconfig ./
COPY src/CrowStation.Domain/*.csproj src/CrowStation.Domain/packages.lock.json src/CrowStation.Domain/
COPY src/CrowStation.Application/*.csproj src/CrowStation.Application/packages.lock.json src/CrowStation.Application/
COPY src/CrowStation.Infrastructure/*.csproj src/CrowStation.Infrastructure/packages.lock.json src/CrowStation.Infrastructure/
COPY src/CrowStation.Api/*.csproj src/CrowStation.Api/packages.lock.json src/CrowStation.Api/
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet restore src/CrowStation.Api/CrowStation.Api.csproj --locked-mode

COPY src/ src/
ARG VERSION=0.0.0-local
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish src/CrowStation.Api/CrowStation.Api.csproj \
      --configuration Release \
      --no-restore \
      --output /app \
      -p:Version=${VERSION} \
      -p:ContinuousIntegrationBuild=true \
      -p:UseAppHost=false

# ---- runtime --------------------------------------------------------------
# Chiseled image: no shell, no package manager, runs as a non-root user by default.
FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble-chiseled AS runtime
WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS=8080 \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_gcServer=1

COPY --from=build /app ./

EXPOSE 8080
USER $APP_UID
ENTRYPOINT ["dotnet", "CrowStation.Api.dll"]
