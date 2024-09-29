namespace Panorama.Backing.Bus.Shared.Common.Eto;

public record Eto
{
    public string MessageId { get; init; }
    public string TenantCorrelationId { get; init; }
    public string UserCorrelationId { get; init; }
    public string CorrelationId { get; init; }
    Guid CommandId { get; }
    DateTime Timestamp { get; }
}