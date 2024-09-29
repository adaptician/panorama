namespace Panorama.Backing.Shared.Common
{
    public interface ICorrelateTenant
    {
        string TenantCorrelationId { get; set; }
        string TenantId { get; set; }
    }
}