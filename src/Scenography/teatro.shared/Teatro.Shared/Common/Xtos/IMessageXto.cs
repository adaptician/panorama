namespace Teatro.Shared.Common.Xtos;

public interface IMessageXto
{
    string MessageId { get; init; }
    
    string TenantCorrelationId { get; init; }
    
    string UserCorrelationId { get; init; }
    
    string CorrelationId { get; init; }
    
    Guid CommandId { get; }
    
    DateTime Timestamp { get; }
};