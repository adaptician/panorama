namespace Panorama.Backing.Shared.Common
{
    public interface IResultEto
    {
        string Operation { get; set; }
        
        string Data { get; set; }
        
        bool HasError { get; set; }
        
        IErrorEto Error { get; set; }
    }
    
    public interface IErrorEto
    {
        string Message { get; set; }
        
        string Decision { get; set; }
    }
}