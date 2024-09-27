using Panorama.Backing.Common;

namespace Panorama.Backing.Brokers;

public abstract class Queues : ReflectToList<Queue>
{
    private static class DefinedQueues
    {
        public static Queue ScenesGetAll = new (QueueNames.ScenesGetAll);
        public static Queue ScenesGet = new (QueueNames.ScenesGet);
        public static Queue SceneCreate = new (QueueNames.SceneCreate);
        public static Queue SceneUpdate = new (QueueNames.SceneUpdate);
        public static Queue SceneDelete = new (QueueNames.SceneDelete);
    }
    
    public static class QueueNames
    {
        public const string ScenesGetAll = "scenes_getall";
        public const string ScenesGet = "scenes_get";
        public const string SceneCreate = "scene_create";
        public const string SceneUpdate = "scene_update";
        public const string SceneDelete = "scene_delete";
    }
    
    public static List<Queue> GetAll()
    {
        return GetAll([typeof(DefinedQueues)]);
    }
}