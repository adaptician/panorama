FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/Panorama.Web.Host/Panorama.Web.Host.csproj", "src/Panorama.Web.Host/"]
COPY ["src/Panorama.Web.Core/Panorama.Web.Core.csproj", "src/Panorama.Web.Core/"]
COPY ["src/Panorama.Application/Panorama.Application.csproj", "src/Panorama.Application/"]
COPY ["src/Panorama.Core/Panorama.Core.csproj", "src/Panorama.Core/"]
COPY ["src/Panorama.EntityFrameworkCore/Panorama.EntityFrameworkCore.csproj", "src/Panorama.EntityFrameworkCore/"]
WORKDIR "/src/src/Panorama.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/Panorama.Web.Host", "src/Panorama.Web.Host"]
COPY ["src/Panorama.Web.Core", "src/Panorama.Web.Core"]
COPY ["src/Panorama.Application", "src/Panorama.Application"]
COPY ["src/Panorama.Core", "src/Panorama.Core"]
COPY ["src/Panorama.EntityFrameworkCore", "src/Panorama.EntityFrameworkCore"]
WORKDIR "/src/src/Panorama.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Panorama.Web.Host.dll"]
