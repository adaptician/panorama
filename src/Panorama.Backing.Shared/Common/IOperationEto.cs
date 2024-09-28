namespace Panorama.Backing.Shared.Common
{
    public interface IOperation
    {
        string Operation { get; set; }
        
        string Data { get; set; }
    }
}