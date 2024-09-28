using Panorama.Backing.Common;

namespace Panorama.Backing.Brokers;

public abstract class Queues : ReflectToList<Queue>
{
    public static class DefinedQueues
    {
        public static Queue QueryScenesGetAll = new (QueueNames.QueryScenesGetAll);
        public static Queue ResultScenesGetAll = new (QueueNames.ResultScenesGetAll);
        
        // public static Queue QueryScenesGet = new (QueueNames.QueryScenesGet);
        // public static Queue CommandSceneCreate = new (QueueNames.CommandSceneCreate);
        // public static Queue CommandSceneUpdate = new (QueueNames.CommandSceneUpdate);
        // public static Queue CommandSceneDelete = new (QueueNames.CommandSceneDelete);
    }
    
    public static class QueueNames
    {
        public const string QueryScenesGetAll = "query_scenes_getall";
        public const string ResultScenesGetAll = "result_scenes_getall";
        public const string QueryScenesGet = "query_scenes_get"; // TODO: the rest
        public const string CommandSceneCreate = "command_scene_create";
        public const string CommandSceneUpdate = "command_scene_update";
        public const string CommandSceneDelete = "command_scene_delete";
    }
    
    public static List<Queue> GetAll()
    {
        return GetAll([typeof(DefinedQueues)]);
    }
}