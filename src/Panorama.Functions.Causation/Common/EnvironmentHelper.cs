namespace Panorama.Functions.Causation.Common;

public static class EnvironmentHelper
{
    public static bool IsDevelopment() => EnvironmentName() == "Development";
    public static bool IsQa() => EnvironmentName() == "Qa";
    public static bool IsProduction() => EnvironmentName() == "Production";
    public static string EnvironmentName()
    {
        var environmentName = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT");
        if (string.IsNullOrEmpty(environmentName))
        {
            environmentName = "Development";
        }

        return environmentName;
    }
}