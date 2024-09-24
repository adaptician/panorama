FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Release

# Copy application files over to container.
WORKDIR /deployed
COPY ["src/Panorama.Web.Host/Panorama.Web.Host.csproj", "src/Panorama.Web.Host/"]
COPY ["src/Panorama.Web.Core/Panorama.Web.Core.csproj", "src/Panorama.Web.Core/"]
COPY ["src/Panorama.Application/Panorama.Application.csproj", "src/Panorama.Application/"]
COPY ["src/Panorama.Core/Panorama.Core.csproj", "src/Panorama.Core/"]
COPY ["src/Panorama.EntityFrameworkCore/Panorama.EntityFrameworkCore.csproj", "src/Panorama.EntityFrameworkCore/"]

# Restore within the container.
WORKDIR "/deployed/src/Panorama.Web.Host"
RUN dotnet restore 

# Copy restored files.
WORKDIR /deployed
COPY ["src/Panorama.Web.Host", "src/Panorama.Web.Host"]
COPY ["src/Panorama.Web.Core", "src/Panorama.Web.Core"]
COPY ["src/Panorama.Application", "src/Panorama.Application"]
COPY ["src/Panorama.Core", "src/Panorama.Core"]
COPY ["src/Panorama.EntityFrameworkCore", "src/Panorama.EntityFrameworkCore"]

# Build the application.
WORKDIR "/deployed/src/Panorama.Web.Host"
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Publish application.
WORKDIR "/deployed/src/Panorama.Web.Host"
FROM build AS publish
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/published /p:UseAppHost=false --no-restore


FROM base AS final

# Copy release files to working location.
WORKDIR /app
COPY --from=publish /app/published .

# Expose service ports.
EXPOSE 80 443

ENTRYPOINT ["dotnet", "Panorama.Web.Host.dll"]
