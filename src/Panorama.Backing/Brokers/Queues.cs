using Panorama.Backing.Common;

namespace Panorama.Backing.Brokers;

public abstract class Queues : ReflectToList<Queue>
{
    public static class DefinedQueues
    {
        public static Queue ScenesOperation = new (QueueNames.ScenesOperation);
        public static Queue ScenesResult = new (QueueNames.ScenesResult);
    }
    
    public static class QueueNames
    {
        public const string ScenesOperation = "scenes.operation";
        public const string ScenesResult = "scenes.result";
    }
    
    public static List<Queue> GetAll()
    {
        return GetAll([typeof(DefinedQueues)]);
    }
}