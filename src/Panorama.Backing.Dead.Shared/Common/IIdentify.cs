namespace Panorama.Backing.Dead.Shared.Common
{
    public interface IIdentify<T>
    {
        T Id { get; set; }
    }
}