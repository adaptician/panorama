using Panorama.Backing.Common;

namespace Panorama.Backing.Brokers;

public abstract class QueueBindings : ReflectToList<QueueBinding>
{
    private static class DefinedQueueBindings
    {
        public static QueueBinding ScenesGetAll = new (
            Queues.QueueNames.ScenesGetAll, 
            Exchanges.ExchangeNames.ScenesExchange, 
            RouteKeys.GetAll);
        
        public static QueueBinding ScenesGet = new (
            Queues.QueueNames.ScenesGet, 
            Exchanges.ExchangeNames.ScenesExchange, 
            RouteKeys.Get);
        
        public static QueueBinding SceneCreate = new (
            Queues.QueueNames.SceneCreate, 
            Exchanges.ExchangeNames.ScenesExchange, 
            RouteKeys.Create);
        
        public static QueueBinding SceneUpdate = new (
            Queues.QueueNames.SceneUpdate, 
            Exchanges.ExchangeNames.ScenesExchange, 
            RouteKeys.Update);
        
        public static QueueBinding SceneDelete = new (
            Queues.QueueNames.SceneDelete, 
            Exchanges.ExchangeNames.ScenesExchange, 
            RouteKeys.Delete);
    }
    
    public static class RouteKeys
    {
        public const string GetAll = "getall";
        public const string Get = "get";
        public const string Create = "create";
        public const string Update = "update";
        public const string Delete = "delete";
    }
    
    public static List<QueueBinding> GetAll()
    {
        return GetAll([typeof(DefinedQueueBindings)]);
    }
}