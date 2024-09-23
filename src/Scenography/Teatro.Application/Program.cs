using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Teatro.Core.Scenography;
using Teatro.EntityFrameworkCore;
using Teatro.Shared.Options;

var builder = WebApplication.CreateBuilder(args);


// Configure connection strings.
string sqlConnectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<TeatroDbContext>(options =>
    options.UseNpgsql(sqlConnectionString));

string nosqlConnectionString = builder.Configuration.GetConnectionString("MongoDb");
builder.Services.AddSingleton<IMongoClient>(provider => new MongoClient(nosqlConnectionString));


// Add services to the container.


// Add AutoMapper.
builder.Services.AddAutoMapper(typeof(Program));


// Register configuration options for dependency injection.
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection(nameof(MongoDbSettings)));


// Register document managers.
builder.Services.AddScoped<ScenographyDocumentManager>();


builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

builder.Services.AddAuthorization();

app.MapControllers();

app.Run();
