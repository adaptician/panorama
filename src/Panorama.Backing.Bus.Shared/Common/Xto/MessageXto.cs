using Teatro.Contracts.Common.Xtos;

namespace Panorama.Backing.Bus.Shared.Common.Xto;

public record MessageXto : IMessageXto
{
    public string MessageId { get; init; }
    
    public string TenantCorrelationId { get; init; }
    
    public string UserCorrelationId { get; init; }
    
    public string CorrelationId { get; init; }
    
    public Guid CommandId { get; }
    
    public DateTime Timestamp { get; }
}