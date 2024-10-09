using Teatro.Contracts.Common;

namespace Teatro.Contracts;

public static class SceneMessageUrn
{
    public const string TeatroScenes = "Teatro.Scenes";
    
    public const string TeatroScenes_RequestScenes = $"{TeatroScenes}:RequestScenesXto";
    public const string Queue_RequestScenes = $"{TeatroMessageUrn.ShortUriQueue}:RequestScenes";
    public const string TeatroScenes_ScenesRequested = $"{TeatroScenes}:ScenesRequestedXto";
    public const string Queue_ScenesRequested = $"{TeatroMessageUrn.ShortUriQueue}:ScenesRequested";
    
    public const string TeatroScenes_RequestScene = $"{TeatroScenes}:RequestSceneXto";
    public const string Queue_RequestScene = $"{TeatroMessageUrn.ShortUriQueue}:RequestScene";
    public const string TeatroScenes_SceneRequested = $"{TeatroScenes}:SceneRequestedXto";
    public const string Queue_SceneRequested = $"{TeatroMessageUrn.ShortUriQueue}:SceneRequested";
    
    public const string TeatroScenes_CreateScene = $"{TeatroScenes}:CreateSceneXto";
    public const string Queue_CreateScene = $"{TeatroMessageUrn.ShortUriQueue}:CreateScene";
    public const string TeatroScenes_SceneCreated = $"{TeatroScenes}:SceneCreatedXto";
    public const string Queue_SceneCreated = $"{TeatroMessageUrn.ShortUriQueue}:SceneCreated";
    
    public const string TeatroScenes_UpdateScene = $"{TeatroScenes}:UpdateSceneXto";
    public const string Queue_UpdateScene = $"{TeatroMessageUrn.ShortUriQueue}:UpdateScene";
    public const string TeatroScenes_SceneUpdated = $"{TeatroScenes}:SceneUpdatedXto";
    public const string Queue_SceneUpdated = $"{TeatroMessageUrn.ShortUriQueue}:SceneUpdated";
    
    public const string TeatroScenes_DeleteScene = $"{TeatroScenes}:DeleteSceneXto";
    public const string Queue_DeleteScene = $"{TeatroMessageUrn.ShortUriQueue}:DeleteScene";
    public const string TeatroScenes_SceneDeleted = $"{TeatroScenes}:SceneDeletedXto";
    public const string Queue_SceneDeleted = $"{TeatroMessageUrn.ShortUriQueue}:SceneDeleted";
}