namespace Panorama.Common.Attributes;

public class CodeAttribute : System.Attribute
{
    public string Code { get; }

    public CodeAttribute(string code)
    {
        this.Code = code;
    }
}