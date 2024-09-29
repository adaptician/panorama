using System.Reflection;

namespace Panorama.Backing.Dead.Common;

public class ReflectToDictionary<TKey, TValue> 
    where TKey : notnull
{
    protected static Dictionary<TKey, TValue> GetAll(Type[] staticTypes)
    {
        List<FieldInfo> fields = new List<FieldInfo>();
        
        foreach (var staticType in staticTypes)
        {
            if (staticType != null)
            {
                fields.AddRange(staticType.GetFields(BindingFlags.Public | BindingFlags.Static).ToList());
            }
        }
            
        Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
            
        foreach (FieldInfo field in fields)
        {
            // Check if the field is a tuple.
            if (field.FieldType == typeof((TKey, TValue)))
            {
                var tupleValue = (ValueTuple<TKey, TValue>)(field.GetValue(null) 
                                                            ?? throw new NullReferenceException("Unable to resolve reflection - value is null."));

                dictionary.Add(tupleValue.Item1, tupleValue.Item2);
            }
        }
        
        return dictionary;
    }
}