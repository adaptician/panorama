using Newtonsoft.Json;

namespace Panorama.Functions.Causation.Extensions;

public static class CloneExtensions
{
    public static T DeepClone<T>(this T source)
    {
        var serialized = JsonConvert.SerializeObject(source);
        return JsonConvert.DeserializeObject<T>(serialized);
    }
}