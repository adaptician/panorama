export class AppEvents {
    static SignalR_AppEvents_UrlPath: string = 'signalr-app-events';
    static SignalR_AppEvents_Connected: string = 'app.events.connected';
    
    static SignalR_AppEvents_Scenes_Retrieved_Listener: string = "notifyOfScenesRetrieved";
    static SignalR_AppEvents_Scenes_Retrieved_Trigger: string = "app.events.scenes.retrieved";

    static SignalR_AppEvents_Scene_Retrieved_Listener: string = "notifyOfSceneRetrieved";
    static SignalR_AppEvents_Scene_Retrieved_Trigger: string = "app.events.scene.retrieved";

    static SignalR_AppEvents_Scene_Created_Listener: string = "notifyOfSceneCreated";
    static SignalR_AppEvents_Scene_Created_Trigger: string = "app.events.scene.created";

    static SignalR_AppEvents_Scene_Updated_Listener: string = "notifyOfSceneUpdated";
    static SignalR_AppEvents_Scene_Updated_Trigger: string = "app.events.scene.updated";

    static SignalR_AppEvents_Scene_Deleted_Listener: string = "notifyOfSceneDeleted";
    static SignalR_AppEvents_Scene_Deleted_Trigger: string = "app.events.scene.deleted";

    static SignalR_AppEvents_Scene_Errored_Listener: string = "notifyOfSceneErrored";
    static SignalR_AppEvents_Scene_Errored_Trigger: string = "app.events.scene.errored";
}