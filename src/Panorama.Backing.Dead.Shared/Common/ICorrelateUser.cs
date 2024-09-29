namespace Panorama.Backing.Dead.Shared.Common
{
    public interface ICorrelateUser
    {
        string UserCorrelationId { get; set; }
        string UserId { get; set; }
    }
}