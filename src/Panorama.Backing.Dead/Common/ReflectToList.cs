using System.Reflection;

namespace Panorama.Backing.Dead.Common;

public abstract class ReflectToList<TItem>
{
    protected static List<TItem> GetAll(Type[] staticTypes)
    {
        List<FieldInfo> fields = new List<FieldInfo>();
            
        foreach (var staticType in staticTypes)
        {
            if (staticType != null)
            {
                fields.AddRange(staticType.GetFields(BindingFlags.Public | BindingFlags.Static).ToList());
            }
        }
            
        List<TItem> items = new List<TItem>();
            
        foreach (FieldInfo field in fields)
        {
            // Check if the field is literal (constant) and static
            if ((field.IsLiteral || field.IsStatic) && !field.IsInitOnly)
            {
                // Add the constant value to the list
                items.Add((TItem)(field.GetValue(null)
                                  ?? throw new NullReferenceException("Unable to resolve reflection - value is null.")));
            }
        }

        return items;
    }
}