namespace Panorama.Backing.Dead.Shared.Common
{
    public interface ICorrelateMessage
    {
        string MessageId { get; set; }
    }
}