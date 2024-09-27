namespace Panorama.Backing.Shared.Common
{
    public interface ICorrelate
    {
        string CorrelationId { get; set; }
    }
}