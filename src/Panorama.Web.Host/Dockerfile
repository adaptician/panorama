FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /deployed

# Expose service ports.
EXPOSE 80 443

# Copy application files over to container.
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

# Publish application.
WORKDIR "/deployed/src/Panorama.Web.Host"
FROM build AS publish
RUN dotnet publish -c Release -o /app/published --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app
# Copy release files to working location.
COPY --from=publish /app/published .

ENTRYPOINT ["dotnet", "Panorama.Web.Host.dll"]
