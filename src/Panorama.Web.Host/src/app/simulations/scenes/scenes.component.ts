import {Component, Injector} from '@angular/core';
import {ViewSceneDto} from "@shared/service-proxies/scenography/dtos/ViewSceneDto";
import {SceneServiceProxy} from "@shared/service-proxies/service-proxies";
import {FilterablePagedRequestDto} from "@shared/service-proxies/common/dtos/FilterablePagedRequestDto";
import {CreateSimulationDialogComponent} from "@app/simulations/create-simulation/create-simulation-dialog.component";
import {EditSimulationDialogComponent} from "@app/simulations/edit-simulation/edit-simulation-dialog.component";
import {PagedListingComponentBase} from "@shared/paged-listing-component-base";
import {finalize} from "rxjs/operators";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {appModuleAnimation} from "@shared/animations/routerTransition";

@Component({
    selector: 'sim-scenes',
    templateUrl: './scenes.component.html',
    styleUrl: './scenes.component.less',
    animations: [appModuleAnimation()]
})
export class ScenesComponent extends PagedListingComponentBase<ViewSceneDto> {
    scenes: ViewSceneDto[] = [];
    keyword = '';

    constructor(
        injector: Injector,
        private _sceneService: SceneServiceProxy,
        private _modalService: BsModalService
    ) {
        super(injector);
    }

    list(
        request: FilterablePagedRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.keyword = this.keyword;

        this._sceneService
            .getAll(request.keyword, request.skipCount, request.maxResultCount)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result) => {
                // TODO: subscribe to event - I think this one might be on init.
                // this.scenes = result.items;
                // this.showPaging(result, pageNumber);
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
                            // TODO: subscribe to event - I think this one might be on init.
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
            // TODO: remove this?
            // this.refresh(); // refresh definitely not needed.
        });
    }
}
