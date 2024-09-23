FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Panorama.Delta/Panorama.Delta.csproj", "src/Panorama.Delta/"]
RUN dotnet restore "src/Panorama.Delta/Panorama.Delta.csproj"
COPY . .
WORKDIR "/src/src/Panorama.Delta"
RUN dotnet build "Panorama.Delta.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Panorama.Delta.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose service ports.
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Panorama.Delta.dll"]
