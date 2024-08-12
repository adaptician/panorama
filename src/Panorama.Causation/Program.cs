using Panorama.Causation.Options;
using Panorama.Causation.Producers;
using Panorama.Causation.Services;

var builder = WebApplication.CreateBuilder(args);

var configRoot = (IConfigurationRoot) builder.Configuration;
builder.Services.AddSingleton(configRoot);

// Bind environment options.
builder.Services.AddOptions<RabbitMqOptions>()
    .Configure<IConfiguration>((settings, configuration) => 
        configuration.GetSection(RabbitMqOptions.SettingName).Bind(settings));

// Add services to the container.

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRabbitMqProducer, RabbitMqProducer>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();