using Panorama.Backing.Common;
using Panorama.Backing.Shared.RoutingKeys;

namespace Panorama.Backing.Brokers;

public abstract class QueueBindings : ReflectToList<QueueBinding>
{
    private static class DefinedQueueBindings
    {
        public static QueueBinding QueryScenesGetAll = new (
            Queues.QueueNames.QueryScenesGetAll, 
            Exchanges.ExchangeNames.ScenesExchange, 
            RoutingKeys.GetAll);
        
        public static QueueBinding ResultScenesGetAll = new (
            Queues.QueueNames.ResultScenesGetAll, 
            Exchanges.ExchangeNames.ScenesExchange, 
            RoutingKeys.GetAllResult);
        
        // public static QueueBinding ScenesGet = new (
        //     Queues.QueueNames.ScenesGet, 
        //     Exchanges.ExchangeNames.ScenesExchange, 
        //     RoutingKeys.Get);
        //
        // public static QueueBinding SceneCreate = new (
        //     Queues.QueueNames.SceneCreate, 
        //     Exchanges.ExchangeNames.ScenesExchange, 
        //     RoutingKeys.Create);
        //
        // public static QueueBinding SceneUpdate = new (
        //     Queues.QueueNames.SceneUpdate, 
        //     Exchanges.ExchangeNames.ScenesExchange, 
        //     RoutingKeys.Update);
        //
        // public static QueueBinding SceneDelete = new (
        //     Queues.QueueNames.SceneDelete, 
        //     Exchanges.ExchangeNames.ScenesExchange, 
        //     RoutingKeys.Delete);
    }
    
    public static List<QueueBinding> GetAll()
    {
        return GetAll([typeof(DefinedQueueBindings)]);
    }
}