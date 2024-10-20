using Teatro.Contracts.Common;

namespace Teatro.Contracts;

public static class SceneMessageUrn
{
    public const string TeatroScenes = "Teatro.Scenes";
    
    public const string TeatroScenes_RetrieveScenes = $"{TeatroScenes}:RetrieveScenesXto";
    public const string Queue_RetrieveScenes = $"{TeatroMessageUrn.ShortUriQueue}:RetrieveScenes";
    public const string TeatroScenes_ScenesRetrieved = $"{TeatroScenes}:ScenesRetrievedXto";
    public const string Queue_ScenesRetrieved = $"{TeatroMessageUrn.ShortUriQueue}:ScenesRetrieved";
    
    public const string TeatroScenes_RetrieveScene = $"{TeatroScenes}:RetrieveSceneXto";
    public const string Queue_RetrieveScene = $"{TeatroMessageUrn.ShortUriQueue}:RetrieveScene";
    public const string TeatroScenes_SceneRetrieved = $"{TeatroScenes}:SceneRetrievedXto";
    public const string Queue_SceneRetrieved = $"{TeatroMessageUrn.ShortUriQueue}:SceneRetrieved";
    
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