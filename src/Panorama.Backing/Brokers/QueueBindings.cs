using Panorama.Backing.Common;
using Panorama.Backing.Shared.RoutingKeys;

namespace Panorama.Backing.Brokers;

public abstract class QueueBindings : ReflectToList<QueueBinding>
{
    private static class DefinedQueueBindings
    {
        public static QueueBinding ScenesOperation = new (
            Queues.QueueNames.ScenesOperation, 
            Exchanges.ExchangeNames.ScenesExchange, 
            RoutingKeys.GetAll);
        
        public static QueueBinding ScenesResult = new (
            Queues.QueueNames.ScenesResult, 
            Exchanges.ExchangeNames.ScenesExchange, 
            RoutingKeys.GetAllResult);
    }
    
    public static List<QueueBinding> GetAll()
    {
        return GetAll([typeof(DefinedQueueBindings)]);
    }
}