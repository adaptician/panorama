using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Panorama.Functions.Causation.Extensions;
using Panorama.Functions.Causation.Options;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration(builder =>
    {
        // Configure the environment variables and load settings files.
        builder.AddFunctionConfiguration();
    })
    .ConfigureServices((context, services) =>
    {
        // Setup application insights.
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        
        // Avail app settings.
        var configRoot = (IConfigurationRoot) context.Configuration;
        services.AddSingleton(configRoot);
        
        // Add logger.
        services.AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("DefaultLogger"));
        
        // Bind environment setting options.
        services.AddOptions<FeatureManagementOptions>()
            .Configure<IConfiguration>((settings, configuration) => 
                configuration.GetSection(FeatureManagementOptions.SettingName).Bind(settings));
        
        // Add MediatR.
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<Program>());
        
        // Add Automapper.
        services.AddAutoMapper(typeof(Program));
        
        // Http Services
        var mockServicesConfig = new MockedServiceOptions();
        configRoot.GetSection(MockedServiceOptions.SettingName).Bind(mockServicesConfig);
        
        if (mockServicesConfig is { EnableMocks: true })
        {
            // services.AddSingleton<IService, MockedService>();
        }
        else
        {
            // Making use of named HTTP Clients.
            // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-8.0#named-clients
            // services.AddHttpClient<IService, TrueService>();
        }
    })
    .Build();

host.Run();