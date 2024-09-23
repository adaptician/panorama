FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src
COPY ["src/Scenography/Teatro.Application/Teatro.Application.csproj", "src/Scenography/Teatro.Application/"]
COPY ["src/Scenography/Teatro.Core/Teatro.Core.csproj", "src/Scenography/Teatro.Core/"]
COPY ["src/Scenography/Teatro.EntityFrameworkCore/Teatro.EntityFrameworkCore.csproj", "src/Scenography/Teatro.EntityFrameworkCore/"]
COPY ["src/Scenography/Teatro.Shared/Teatro.Shared.csproj", "src/Scenography/Teatro.Shared/"]

# Restore Nuget packages.
RUN dotnet restore "src/Scenography/Teatro.Application/Teatro.Application.csproj"

COPY . .

# Build the application.
WORKDIR "/src/src/Scenography/Teatro.Application"
RUN dotnet build "Teatro.Application.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the built application.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Teatro.Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose service ports.
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Teatro.Application.dll"]
