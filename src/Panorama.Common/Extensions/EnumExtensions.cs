using JetBrains.Annotations;
using Panorama.Common.Attributes;

namespace Panorama.Common.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Get the code tag set on an ENUM.
    /// </summary>
    /// <param name="enumValue">The ENUM in question</param>
    /// <returns></returns>
    [CanBeNull]
    public static string GetCode(this Enum enumValue)
    {
        try
        {
            return GetAttribute<CodeAttribute>(enumValue)?.Code;
        }
        catch
        {
            // Fall back to just the name if Description tag is not set.
            return enumValue.ToString();
        }
    }
    
    /// <summary>
    /// Get the value of a specific attribute type for a given enum.
    /// </summary>
    /// <param name="enumValue">The ENUM in question</param>
    /// <typeparam name="TAttribute">The type of the attribute that should be retrieved.</typeparam>
    /// <returns>The value of the first matching attribute, otherwise null.</returns>
    [CanBeNull]
    public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
        where TAttribute : Attribute
    {
        var type = enumValue.GetType();
        var memInfo = type.GetMember(enumValue.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(TAttribute), false);
        return attributes.Length > 0 ? (TAttribute) attributes[0] : null;
    }
}