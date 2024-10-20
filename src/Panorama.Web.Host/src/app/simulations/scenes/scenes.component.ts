import {Component, Injector, NgZone, OnInit} from '@angular/core';
import {ViewSceneDto} from "@shared/service-proxies/scenography/dtos/ViewSceneDto";
import {PagedSceneResultRequestDto, SceneServiceProxy} from "@shared/service-proxies/service-proxies";
import {CreateSimulationDialogComponent} from "@app/simulations/create-simulation/create-simulation-dialog.component";
import {EditSimulationDialogComponent} from "@app/simulations/edit-simulation/edit-simulation-dialog.component";
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
        this.setBusy('loading', true)

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
            this.l('SimulationDeleteWarningMessage', scene.name),
            undefined,
            (result: boolean) => {
                if (result) {
                    this._sceneService
                        .commandDelete(scene.correlationId)
                        .pipe(finalize(() => this.setBusy('saving', false)))
                        .subscribe(() => {
                        });
                }
            }
        );
    }

    createScene(): void {
        this.showCreateOrEditSimulationDialog();
    }

    editScene(scene: ViewSceneDto): void {
        this.showCreateOrEditSimulationDialog(scene.correlationId);
    }

    showCreateOrEditSimulationDialog(correlationId?: string): void {
        let createOrEditSimulationDialog: BsModalRef;
        if (!correlationId || correlationId.length == 0) {
            createOrEditSimulationDialog = this._modalService.show(
                CreateSimulationDialogComponent,
                {
                    class: 'modal-lg',
                }
            );
        } else {
            createOrEditSimulationDialog = this._modalService.show(
                EditSimulationDialogComponent,
                {
                    class: 'modal-lg',
                    initialState: {
                        sceneCorrelationId: correlationId,
                    },
                }
            );
        }

        createOrEditSimulationDialog.content.onSave.subscribe(() => {
            this.setBusy('loading', true);
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

            this.setBusy('loading', false);
            this.refresh();
        }
    }

    private handleSceneUpdated(data: SceneUpdatedEventData): void {

        if (data?.data) {
            const result = data.data;

            this.setBusy('loading', false);
            this.refresh();
        }
    }

    private handleSceneDeleted(data: SceneDeletedEventData): void {

        if (data) {

            this.setBusy('loading', false);
            this.refresh();
        }
    }

    private handleSceneErrored(data: SceneErroredEventData): void {

        this.setBusy('loading', false);
        abp.message.error(
            this.l('MessageConsumptionFailed', data?.error?.errorMessage),
            undefined,
            () => {
                this.refresh();
            }
        );
    }
}
