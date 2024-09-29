using Panorama.Backing.Dead.Common;
using Panorama.Backing.Dead.Shared.RoutingKeys;

namespace Panorama.Backing.Dead.Brokers;

public abstract class QueueBindings : ReflectToList<QueueBinding>
{
    private static class DefinedQueueBindings
    {
        public static QueueBinding ScenesOperation = new (
            Queues.QueueNames.ScenesOperation, 
            Exchanges.ExchangeNames.ScenesExchange, 
            RoutingKeys.Operation);
        
        public static QueueBinding ScenesResult = new (
            Queues.QueueNames.ScenesResult, 
            Exchanges.ExchangeNames.ScenesExchange, 
            RoutingKeys.OperationResult);
    }
    
    public static List<QueueBinding> GetAll()
    {
        return GetAll([typeof(DefinedQueueBindings)]);
    }
}