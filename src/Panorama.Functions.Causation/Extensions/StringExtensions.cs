namespace Panorama.Functions.Causation.Extensions;

public static class StringExtensions
{
    public static string GetTrailingCharacters(this string value, int numberOfCharacters = 4)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;

        if (value.Length <= numberOfCharacters) return value;

        return value.Substring(value.Length - numberOfCharacters - 1, numberOfCharacters);
    }
}