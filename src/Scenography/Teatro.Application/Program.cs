using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Panorama.Backing.ConnectionPools;
using Panorama.Backing.Consumers;
using Panorama.Backing.Options;
using Panorama.Backing.Producers;
using Panorama.Backing.Shared.Consumers;
using Panorama.Backing.Shared.Messages;
using Panorama.Backing.Shared.Scenes.Requests.Eto;
using Panorama.Backing.Workers;
using Panorama.Common.Enums;
using Panorama.Common.Extensions;
using Panorama.Common.Repositories;
using Teatro.Application.Handlers;
using Teatro.Core.Scenes;
using Teatro.Core.Scenography;
using Teatro.EntityFrameworkCore;
using Teatro.EntityFrameworkCore.Repositories;
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

builder.Services.AddScoped<IQueryableRepository<Scene, long>, SceneRepository>();


#region Add Event Bus

var eventBusSection = builder.Configuration.GetSection(EventBusOptions.SettingName);
builder.Services.Configure<EventBusOptions>(eventBusSection);
            
EventBusOptions eventBusOptions = new EventBusOptions();
eventBusSection.Bind(eventBusOptions);
            
if (eventBusOptions.BusType.Equals(EventBusTypeEnum.RabbitMq.GetCode()))
{
    builder.Services.AddSingleton<IRabbitMqConnectionPool, RabbitMqConnectionPool>();
    
    builder.Services.AddSingleton<IProcessMessageHandler<ScenesRequestedEto>, ScenesRequestedHandler>();
    builder.Services.AddSingleton<IConsumer<ScenesRequestedEto>, ScenesConsumer>();
    builder.Services.AddTransient<ScenesProducer>();

    builder.Services.AddHostedService<ScenesConsumerWorker>();
}

#endregion


builder.Services.AddAuthorization();
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


// Configure CORS for angular2 UI.
string CorsOriginStringArray = builder.Configuration.GetSection("CorsOrigins").Value ?? string.Empty;
app.UseCors(builder => builder
    .WithOrigins(
        CorsOriginStringArray
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(o => o.TrimEnd(['/']))
            .ToArray()
        )
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);


app.MapControllers();


app.Run();
