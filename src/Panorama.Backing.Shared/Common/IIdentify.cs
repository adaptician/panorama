namespace Panorama.Backing.Shared.Common
{
    public interface IIdentify<T>
    {
        T Id { get; set; }
    }
}