import {Component, Injector} from '@angular/core';
import {appModuleAnimation} from "@shared/animations/routerTransition";
import {ViewSceneDto} from "@shared/service-proxies/scenography/dtos/ViewSceneDto";
import {FilterablePagedRequestDto} from "@shared/service-proxies/common/dtos/FilterablePagedRequestDto";
import {PagedListingComponentBase} from "@shared/paged-listing-component-base";
import {SceneServiceProxy} from "@shared/service-proxies/scenography/scenography.service-proxies";
import {finalize} from "rxjs/operators";
import {PagedResultDto} from "@shared/service-proxies/common/dtos/PagedResultDto";
import {
    CreateSimulationDialogComponent
} from "@app/simulations/create-simulation-dialog/create-simulation-dialog.component";
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';


@Component({
    selector: 'sim-simulations',
    templateUrl: './simulations.component.html',
    styleUrl: './simulations.component.less',
    animations: [appModuleAnimation()]
})
export class SimulationsComponent extends PagedListingComponentBase<ViewSceneDto> {

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
            .subscribe((result: PagedResultDto<ViewSceneDto>) => {
                this.scenes = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    delete(scene: ViewSceneDto): void {
        // abp.message.confirm(
        //     this.l('RoleDeleteWarningMessage', role.displayName),
        //     undefined,
        //     (result: boolean) => {
        //         if (result) {
        //             this._rolesService
        //                 .delete(role.id)
        //                 .pipe(
        //                     finalize(() => {
        //                         abp.notify.success(this.l('SuccessfullyDeleted'));
        //                         this.refresh();
        //                     })
        //                 )
        //                 .subscribe(() => {});
        //         }
        //     }
        // );
    }

    createScene(): void {
        this.showCreateOrEditSimulationDialog();
    }

    editScene(scene: ViewSceneDto): void {
        // this.showCreateOrEditRoleDialog(role.id);
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
            // createOrEditSceneDialog = this._modalService.show(
            //     EditSceneDialogComponent,
            //     {
            //         class: 'modal-lg',
            //         initialState: {
            //             id: id,
            //         },
            //     }
            // );
        }

        createOrEditSimulationDialog.content.onSave.subscribe(() => {
            this.refresh();
        });
    }
}
