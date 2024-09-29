namespace Panorama.Backing.Dead.Shared.Common
{
    public interface ICorrelate
    {
        string CorrelationId { get; set; }
    }
}