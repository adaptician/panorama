import {Component, Injector, NgZone, OnInit} from '@angular/core';
import {ViewSceneDto} from "@shared/service-proxies/scenography/dtos/ViewSceneDto";
import {SceneServiceProxy} from "@shared/service-proxies/service-proxies";
import {FilterablePagedRequestDto} from "@shared/service-proxies/common/dtos/FilterablePagedRequestDto";
import {CreateSimulationDialogComponent} from "@app/simulations/create-simulation/create-simulation-dialog.component";
import {EditSimulationDialogComponent} from "@app/simulations/edit-simulation/edit-simulation-dialog.component";
import {PagedListingComponentBase} from "@shared/paged-listing-component-base";
import {finalize} from "rxjs/operators";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {appModuleAnimation} from "@shared/animations/routerTransition";
import {AppEvents} from "@shared/AppEvents";
import {ScenesReceivedEventData} from "@shared/service-proxies/scenography/events/ScenesReceivedEventData";

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
        request: FilterablePagedRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.keyword = this.keyword;
        this.setBusy('saving', true)

        this._sceneService
            .getAll(request.keyword, request.skipCount, request.maxResultCount)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result) => {
                this.scenes = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    delete(scene: ViewSceneDto): void {
        abp.message.confirm(
            this.l('SimulationDeleteWarningMessage', scene.name),
            undefined,
            (result: boolean) => {
                if (result) {
                    this._sceneService
                        .delete(scene.id)
                        .pipe(
                            finalize(() => {
                                abp.notify.success(this.l('SuccessfullyDeleted'));
                                this.refresh();
                            })
                        )
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
        this.showCreateOrEditSimulationDialog(scene.id);
    }

    showCreateOrEditSimulationDialog(id?: number): void {
        let createOrEditSimulationDialog: BsModalRef;
        if (!id) {
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
                        sceneId: id,
                    },
                }
            );
        }

        createOrEditSimulationDialog.content.onSave.subscribe(() => {
            this.refresh();
        });
    }

    // TODO:T clean this up eish.
    private subscribeToEvents(): void {

        this.subscribeToEvent(AppEvents.SignalR_AppEvents_Connected, () => {
            this._zone.run(() => {
                this.notify.info(this.l('IAmListeningForEvents'));
            });
        });

        this.subscribeToEvent(AppEvents.SignalR_AppEvents_Scenes_Received_Trigger,
            (json) => {

                const data = new ScenesReceivedEventData();
                Object.assign(data, JSON.parse(json));

                this._zone.run(() => {
                    this.handleScenesReceived(data);
                });
            });

    }

    private handleScenesReceived(data: ScenesReceivedEventData): void {
        console.log(`DATA ${JSON.stringify(data)}`);
        const x = "let the madness begin";

        this.scenes = data.Data.items;
        this.setBusy('saving', false)
    }
}
