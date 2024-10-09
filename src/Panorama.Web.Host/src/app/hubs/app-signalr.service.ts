
import {HubConnection} from "@aspnet/signalr";
import {Injectable, Injector, NgZone} from "@angular/core";
import {AppComponentBase} from "@shared/app-component-base";
import {AppEvents} from "@shared/AppEvents";

@Injectable()
export class AppSignalrService extends AppComponentBase {

    hub: HubConnection;
    private _isConnected = false;
    
    constructor(
        injector: Injector,
        public _zone: NgZone
    ) {
        super(injector);
    }
    
    public get isConnected(): boolean {
        return this._isConnected;
    }

    configureConnection(connection): void {
        // Set the common hub
        this.hub = connection;

        // Reconnect if hub disconnects
        connection.onclose(e => {
            this._isConnected = false;
            if (e) {
                abp.log.debug('Casting connection closed with error: ' + e);
            } else {
                abp.log.debug('Casting disconnected');
            }

            if (!abp.signalr.autoConnect) {
                return;
            }

            setTimeout(() => {
                connection.start().then(result => {
                    this._isConnected = true;
                });
            }, 5000);
        });

        // Register to get notifications
        this.registerEvents(connection);
    }

    registerEvents(connection): void {
        connection.on(AppEvents.SignalR_AppEvents_Scenes_Received_Listener, (event) => {
            abp.event.trigger(AppEvents.SignalR_AppEvents_Scenes_Received_Trigger, event);
        });

        connection.on(AppEvents.SignalR_AppEvents_Scene_Received_Listener, (event) => {
            abp.event.trigger(AppEvents.SignalR_AppEvents_Scene_Received_Trigger, event);
        });

        connection.on(AppEvents.SignalR_AppEvents_Scene_Created_Listener, (event) => {
            abp.event.trigger(AppEvents.SignalR_AppEvents_Scene_Created_Trigger, event);
        });

        connection.on(AppEvents.SignalR_AppEvents_Scene_Updated_Listener, (event) => {
            abp.event.trigger(AppEvents.SignalR_AppEvents_Scene_Updated_Trigger, event);
        });

        connection.on(AppEvents.SignalR_AppEvents_Scene_Deleted_Listener, (event) => {
            abp.event.trigger(AppEvents.SignalR_AppEvents_Scene_Deleted_Trigger, event);
        });
    }

    init(): void {
        this._zone.runOutsideAngular(() => {
            abp.signalr.connect();
            abp.signalr
                .startConnection(abp.appPath + AppEvents.SignalR_AppEvents_UrlPath, (connection) => {
                    abp.event.trigger(AppEvents.SignalR_AppEvents_Connected);
                    this._isConnected = true;
                    this.configureConnection(connection);
            });
        });
    }
}