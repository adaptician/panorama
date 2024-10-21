import {Component, Injector, NgZone, OnInit} from '@angular/core';
import {ViewSceneDto} from "@shared/service-proxies/scenography/dtos/ViewSceneDto";
import {PagedSceneResultRequestDto, SceneServiceProxy} from "@shared/service-proxies/service-proxies";
import {PagedListingComponentBase} from "@shared/paged-listing-component-base";
import {finalize} from "rxjs/operators";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {appModuleAnimation} from "@shared/animations/routerTransition";
import {AppEvents} from "@shared/AppEvents";
import {ScenesRetrievedEventData} from "@shared/service-proxies/scenography/events/ScenesRetrievedEventData";
import {SceneCreatedEventData} from "@shared/service-proxies/scenography/events/SceneCreatedEventData";
import {SceneUpdatedEventData} from "@shared/service-proxies/scenography/events/SceneUpdatedEventData";
import {SceneDeletedEventData} from "@shared/service-proxies/scenography/events/SceneDeletedEventData";
import {SceneErroredEventData} from "@shared/service-proxies/scenography/events/SceneErroredEventData";
import {CreateSceneDialogComponent} from "@app/scenes/create-scene/create-scene-dialog.component";
import {EditSceneDialogComponent} from "@app/scenes/edit-simulation/edit-scene-dialog.component";

@Component({
    selector: 'sim-scenes',
    templateUrl: './scenes.component.html',
    styleUrl: './scenes.component.less',
    animations: [appModuleAnimation()]
})
export class ScenesComponent extends PagedListingComponentBase<ViewSceneDto> implements OnInit {
    
    scenes: ViewSceneDto[] = [];
    keyword = '';

    constructor(
        injector: Injector,
        private _sceneService: SceneServiceProxy,
        private _modalService: BsModalService,
        private _zone: NgZone
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.subscribeToEvents();
    }

    list(
        request: PagedSceneResultRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.keyword = this.keyword;
        this.setBusy('loading', true);

        this._sceneService
            .commandGetAll(request)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result) => {});
    }

    delete(scene: ViewSceneDto): void {
        abp.message.confirm(
            this.l('SceneDeleteWarningMessage', scene.name),
            undefined,
            (result: boolean) => {
                if (result) {
                    this.setBusy('saving', true);
                    
                    this._sceneService
                        .commandDelete(scene.correlationId)
                        .subscribe(() => {
                        });
                }
            }
        );
    }

    createScene(): void {
        this.showCreateOrEditSceneDialog();
    }

    editScene(scene: ViewSceneDto): void {
        this.showCreateOrEditSceneDialog(scene.correlationId);
    }

    showCreateOrEditSceneDialog(correlationId?: string): void {
        let createOrEditSceneDialog: BsModalRef;
        if (!correlationId || correlationId.length == 0) {
            createOrEditSceneDialog = this._modalService.show(
                CreateSceneDialogComponent,
                {
                    class: 'modal-lg',
                }
            );
        } else {
            createOrEditSceneDialog = this._modalService.show(
                EditSceneDialogComponent,
                {
                    class: 'modal-lg',
                    initialState: {
                        sceneCorrelationId: correlationId,
                    },
                }
            );
        }

        createOrEditSceneDialog.content.onSave.subscribe(() => {
            this.setBusy('saving', true);
        });
    }

    private subscribeToEvents(): void {

        this.subscribeToEvent(AppEvents.SignalR_AppEvents_Scenes_Retrieved_Trigger,
            (json) => {

                const data = new ScenesRetrievedEventData();
                Object.assign(data, JSON.parse(json));

                this._zone.run(() => {
                    this.handleScenesReceived(data);
                });
            });

        this.subscribeToEvent(AppEvents.SignalR_AppEvents_Scene_Created_Trigger,
            (json) => {

                const data = new SceneCreatedEventData();
                Object.assign(data, JSON.parse(json));
            
                this._zone.run(() => {
                    this.handleSceneCreated(data);
                });
            });

        this.subscribeToEvent(AppEvents.SignalR_AppEvents_Scene_Updated_Trigger,
            (json) => {

                const data = new SceneUpdatedEventData();
                Object.assign(data, JSON.parse(json));

                this._zone.run(() => {
                    this.handleSceneUpdated(data);
                });
            });

        this.subscribeToEvent(AppEvents.SignalR_AppEvents_Scene_Deleted_Trigger,
            (json) => {

                const data = new SceneDeletedEventData();
                Object.assign(data, JSON.parse(json));

                this._zone.run(() => {
                    this.handleSceneDeleted(data);
                });
            });

        this.subscribeToEvent(AppEvents.SignalR_AppEvents_Scene_Errored_Trigger,
            (json) => {

                const data = new SceneErroredEventData();
                Object.assign(data, JSON.parse(json));

                this._zone.run(() => {
                    this.handleSceneErrored(data);
                });
            });
        
    }

    private handleScenesReceived(data: ScenesRetrievedEventData): void {
        
        if (data?.data) {
            const result = data.data;

            this.scenes = result.items;
            this.totalItems = result.totalCount;
            this.showPaging(result, this.pageNumber);
            this.setBusy('loading', false);   
        }
    }

    private handleSceneCreated(data: SceneCreatedEventData): void {

        if (data?.data) {
            const result = data.data;

            this.setBusy('saving', false);
            this.refresh();
        }
    }

    private handleSceneUpdated(data: SceneUpdatedEventData): void {

        if (data?.data) {
            const result = data.data;

            this.setBusy('saving', false);
            this.refresh();
        }
    }

    private handleSceneDeleted(data: SceneDeletedEventData): void {

        if (data) {

            this.setBusy('saving', false);
            this.refresh();
        }
    }

    private handleSceneErrored(data: SceneErroredEventData): void {

        this.resetBusy();
        
        abp.message.error(
            this.l('MessageConsumptionFailed', data?.error?.errorMessage),
            undefined,
            () => {
                this.refresh();
            }
        );
    }
}
