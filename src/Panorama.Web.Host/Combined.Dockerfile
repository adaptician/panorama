## ABP API and APP must be hosted together in the same container, 
## otherwise Session data does not carry to the backend, if they are in different pods.

## --- Stage 1 - Angular App -------------------------------
FROM node:20 AS app-build

# Set working directory.
WORKDIR /app

# Install dependencies.
COPY panorama/src/Panorama.Web.Host/package*.json ./
RUN yarn install

# Copy the rest of the application files.
COPY panorama/src/Panorama.Web.Host .

# Build the Angular app.
RUN yarn run ng build --configuration production

## -- END APP -------------------------------------------

## --- Stage 2 - WEB API --------------------------------

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS api-base
USER $APP_UID

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS api-build

ARG BUILD_CONFIGURATION=Release

# Copy application files over to container.
WORKDIR /deployed
COPY ["panorama/src/Scenography/teatro.shared/Teatro.Contracts/Teatro.Contracts.csproj", "src/Scenography/teatro.shared/Teatro.Contracts/"]

COPY ["panorama/src/Panorama.Application/Panorama.Application.csproj", "src/Panorama.Application/"]

COPY ["panorama/src/Panorama.Backing.Bus/Panorama.Backing.Bus.csproj", "src/Panorama.Backing.Bus/"]
COPY ["panorama/src/Panorama.Backing.Bus.Shared/Panorama.Backing.Bus.Shared.csproj", "src/Panorama.Backing.Bus.Shared/"]

COPY ["panorama/src/Panorama.Common/Panorama.Common.csproj", "src/Panorama.Common/"]

COPY ["panorama/src/Panorama.Core/Panorama.Core.csproj", "src/Panorama.Core/"]
COPY ["panorama/src/Panorama.EntityFrameworkCore/Panorama.EntityFrameworkCore.csproj", "src/Panorama.EntityFrameworkCore/"]

COPY ["panorama/src/Panorama.Web.Core/Panorama.Web.Core.csproj", "src/Panorama.Web.Core/"]

COPY ["panorama/src/Panorama.Web.Host/Panorama.Web.Host.csproj", "src/Panorama.Web.Host/"]

# Restore within the container.
WORKDIR "/deployed/src/Panorama.Web.Host"
RUN dotnet restore 

# Copy restored files.
WORKDIR /deployed
COPY ["panorama/src/Scenography/teatro.shared/Teatro.Contracts", "src/Scenography/teatro.shared/Teatro.Contracts"]

COPY ["panorama/src/Panorama.Application", "src/Panorama.Application"]

COPY ["panorama/src/Panorama.Backing.Bus", "src/Panorama.Backing.Bus"]
COPY ["panorama/src/Panorama.Backing.Bus.Shared", "src/Panorama.Backing.Bus.Shared"]

COPY ["panorama/src/Panorama.Common", "src/Panorama.Common"]

COPY ["panorama/src/Panorama.Core", "src/Panorama.Core"]
COPY ["panorama/src/Panorama.EntityFrameworkCore", "src/Panorama.EntityFrameworkCore"]

COPY ["panorama/src/Panorama.Web.Core", "src/Panorama.Web.Core"]

COPY ["panorama/src/Panorama.Web.Host", "src/Panorama.Web.Host"]

# Build the application.
WORKDIR "/deployed/src/Panorama.Web.Host"
RUN dotnet build -c $BUILD_CONFIGURATION -o /api/build

# Publish application.
WORKDIR "/deployed/src/Panorama.Web.Host"
FROM api-build AS api-publish
RUN dotnet publish -c $BUILD_CONFIGURATION -o /api/published /p:UseAppHost=false --no-restore


FROM api-base AS final

# Copy release files to working location.
WORKDIR /app
COPY --from=api-publish /api/published .
COPY --from=app-build /app/wwwroot/dist /app/wwwroot/

# Expose service ports.
EXPOSE 80 443

ENTRYPOINT ["dotnet", "Panorama.Web.Host.dll"]
