namespace Teatro.Contracts.Common.Xtos;

public interface IMessageXto
{
    string MessageId { get; }
    
    string TenantCorrelationId { get; }
    
    string UserCorrelationId { get; }
    
    string CorrelationId { get; }
    
    Guid CommandId { get; }
    
    DateTime Timestamp { get; }
};