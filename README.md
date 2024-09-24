# Introduction

This is a template to create **ASP.NET Core Angular** based startup projects for [ASP.NET Boilerplate](https://aspnetboilerplate.com/Pages/Documents).

[ASP.NET Core & Angular](https://aspnetboilerplate.com/Pages/Documents/Zero/Startup-Template-Angular) (single page application).
 
User Interface is based on [AdminLTE theme](https://github.com/ColorlibHQ/AdminLTE).

# Boilerplate Documentation

* [ASP.NET Core MVC & jQuery version.](https://aspnetboilerplate.com/Pages/Documents/Zero/Startup-Template-Core)
* [ASP.NET Core & Angular  version.](https://aspnetboilerplate.com/Pages/Documents/Zero/Startup-Template-Angular)

# License

[MIT](LICENSE).

# Setup

TODO: see kubernetes file

# Running Database Migrations
The solution is following a mono-repo pattern, and includes multiple microservices, some of which include storage.

## Panorama
The Panorama projects are the ABP Boilerplate projects, and are configured to persist data to a SQL Server instance.

From project root:
`cd src`
`dotnet ef database update --project Panorama.EntityFrameworkCore --startup-project Panorama.Web.Host`

Alternatively, you can run the migrator project.

## Teatro
The Teatro projects are a custom web API, and are configured to persist data to a Postgres instance.

From project root:
`cd src`
`dotnet ef database update --project Scenography/Teatro.EntityFrameworkCore --startup-project Scenography/Teatro.Application`
