using Microsoft.EntityFrameworkCore;
using Teatro.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configRoot = (IConfigurationRoot) builder.Configuration;
builder.Services.AddSingleton(configRoot);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string sqlConnectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<TeatroDbContext>(options =>
    options.UseNpgsql(sqlConnectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// TODO: apply authorization?
// app.UseAuthorization();

app.Run();
