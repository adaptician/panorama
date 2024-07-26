FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Panorama.Migrator/Panorama.Migrator.csproj", "src/Panorama.Migrator/"]
COPY ["src/Panorama.Core/Panorama.Core.csproj", "src/Panorama.Core/"]
COPY ["src/Panorama.EntityFrameworkCore/Panorama.EntityFrameworkCore.csproj", "src/Panorama.EntityFrameworkCore/"]
RUN dotnet restore "src/Panorama.Migrator/Panorama.Migrator.csproj"
COPY . .
WORKDIR "/src/src/Panorama.Migrator"
RUN dotnet build "Panorama.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Panorama.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Panorama.Migrator.dll"]

# Need to interact with shell to choose "Y" on migration question.
# Can't get this working with
# ENTRYPOINT ["dotnet", "Panorama.Migrator.dll", "-s"]
# ENTRYPOINT ["dotnet", "Panorama.Migrator.dll", "bash/sh/", "Y"]
# OR combinations of ENTRYPOINT and CMD
# ENTRYPOINT ["dotnet", "Panorama.Migrator.dll"]
# CMD ["Y"]

# As a workaround, I have modified the boilerplate executable to default to "Y" if no input is received.