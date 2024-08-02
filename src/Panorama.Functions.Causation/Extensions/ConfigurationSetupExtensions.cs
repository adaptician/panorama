using Microsoft.Extensions.Configuration;
using Panorama.Functions.Causation.Common;

namespace Panorama.Functions.Causation.Extensions;

public static class ConfigurationSetupExtensions
{
    public static IConfigurationBuilder AddFunctionConfiguration(this IConfigurationBuilder builder)
    {
        return builder
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{EnvironmentHelper.EnvironmentName()}.json", true, true)
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    }
}