FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Release

# Copy application files over to container.
WORKDIR /deployed
COPY ["panorama/src/Scenography/teatro.shared/Teatro.Contracts/Teatro.Contracts.csproj", "src/Scenography/teatro.shared/Teatro.Contracts/"]

COPY ["panorama/src/Panorama.Backing.Bus.Shared/Panorama.Backing.Bus.Shared.csproj", "src/Panorama.Backing.Bus.Shared/"]

COPY ["panorama/src/Panorama.Core/Panorama.Core.csproj", "src/Panorama.Core/"]

COPY ["panorama/src/Panorama.EntityFrameworkCore/Panorama.EntityFrameworkCore.csproj", "src/Panorama.EntityFrameworkCore/"]

COPY ["panorama/src/Panorama.Migrator/Panorama.Migrator.csproj", "src/Panorama.Migrator/"]

# Restore within the container.
WORKDIR "/deployed/src/Panorama.Migrator"
RUN dotnet restore 

# Copy restored files.
WORKDIR /deployed
COPY ["panorama/src/Scenography/teatro.shared/Teatro.Contracts", "src/Scenography/teatro.shared/Teatro.Contracts"]

COPY ["panorama/src/Panorama.Backing.Bus.Shared", "src/Panorama.Backing.Bus.Shared"]

COPY ["panorama/src/Panorama.Core", "src/Panorama.Core"]

COPY ["panorama/src/Panorama.EntityFrameworkCore", "src/Panorama.EntityFrameworkCore"]

COPY ["panorama/src/Panorama.Migrator", "src/Panorama.Migrator"]

# Build the application.
WORKDIR "/deployed/src/Panorama.Migrator"
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Publish application.
WORKDIR "/deployed/src/Panorama.Migrator"
FROM build AS publish
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/published /p:UseAppHost=false --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/published .

ENTRYPOINT ["dotnet", "Panorama.Migrator.dll"]

# Need to interact with shell to choose "Y" on migration question.
# Can't get this working with
# ENTRYPOINT ["dotnet", "Panorama.Migrator.dll", "-s"]
# ENTRYPOINT ["dotnet", "Panorama.Migrator.dll", "bash/sh/", "Y"]
# OR combinations of ENTRYPOINT and CMD
# ENTRYPOINT ["dotnet", "Panorama.Migrator.dll"]
# CMD ["Y"]

# As a workaround, I have modified the boilerplate executable to default to "Y" if no input is received.