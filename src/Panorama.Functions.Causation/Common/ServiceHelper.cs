using System.Text;

namespace Panorama.Functions.Causation.Common;

public static class ServiceHelper
{
    public static string BuildUrl(string[] pathArgs)
    {
        var stringBuilder = new StringBuilder();
        foreach (string arg in pathArgs)
        {
            if (string.IsNullOrEmpty(arg))
                continue;

            stringBuilder.Append(arg);

            if (!arg.EndsWith('/'))
                stringBuilder.Append('/');
        }

        return stringBuilder.ToString();
    }

    public static string EnsureEndsWithSlash(this string url)
    {
        if (!url.EndsWith('/'))
            return $"{url}/";

        return url;
    }
}