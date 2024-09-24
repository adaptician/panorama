FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Release

# Copy application files over to container.
WORKDIR /deployed
COPY ["src/Scenography/Teatro.Application/Teatro.Application.csproj", "src/Scenography/Teatro.Application/"]
COPY ["src/Scenography/Teatro.Core/Teatro.Core.csproj", "src/Scenography/Teatro.Core/"]
COPY ["src/Scenography/Teatro.EntityFrameworkCore/Teatro.EntityFrameworkCore.csproj", "src/Scenography/Teatro.EntityFrameworkCore/"]
COPY ["src/Scenography/Teatro.Shared/Teatro.Shared.csproj", "src/Scenography/Teatro.Shared/"]

# Restore within the container.
WORKDIR "/deployed/src/Scenography/Teatro.Application"
RUN dotnet restore 

# Copy restored files.
WORKDIR /deployed
COPY . .

# Build the application.
WORKDIR "/deployed/src/Scenography/Teatro.Application"
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Publish the application.
WORKDIR "/deployed/src/Scenography/Teatro.Application"
FROM build AS publish
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/published /p:UseAppHost=false --no-restore

FROM base AS final

# Copy release files to working location.
WORKDIR /app
COPY --from=publish /app/published .

# Expose service ports.
EXPOSE 80 443

ENTRYPOINT ["dotnet", "Teatro.Application.dll"]
