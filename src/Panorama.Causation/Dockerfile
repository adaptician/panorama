FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Panorama.Causation/Panorama.Causation.csproj", "src/Panorama.Causation/"]
RUN dotnet restore "src/Panorama.Causation/Panorama.Causation.csproj"
COPY . .
WORKDIR "/src/src/Panorama.Causation"
RUN dotnet build "Panorama.Causation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Panorama.Causation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose service ports.
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Panorama.Causation.dll"]
