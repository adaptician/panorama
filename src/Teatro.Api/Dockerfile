FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src
COPY ["src/Teatro.Api/Teatro.Api.csproj", "src/Teatro.Api/"]
COPY ["src/Teatro.Shared/Teatro.Shared.csproj", "src/Teatro.Shared/"]
COPY ["src/Teatro.Core/Teatro.Core.csproj", "src/Teatro.Core/"]
COPY ["src/Teatro.EntityFrameworkCore/Teatro.EntityFrameworkCore.csproj", "src/Teatro.EntityFrameworkCore/"]

# Restore Nuget packages.
RUN dotnet restore "src/Teatro.Api/Teatro.Api.csproj"

COPY . .

# Build the application.
WORKDIR "/src/src/Teatro.Api"
RUN dotnet build "Teatro.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the built application.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "Teatro.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose service ports.
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Teatro.Api.dll"]
