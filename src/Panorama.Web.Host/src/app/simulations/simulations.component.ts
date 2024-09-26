import {Component, Injector} from '@angular/core';
import {appModuleAnimation} from "@shared/animations/routerTransition";
import {ViewSceneDto} from "@shared/service-proxies/scenography/dtos/ViewSceneDto";
import {FilterablePagedRequestDto} from "@shared/service-proxies/common/dtos/FilterablePagedRequestDto";
import {PagedListingComponentBase} from "@shared/paged-listing-component-base";


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
    ) {
        super(injector);
    }

    list(
        request: FilterablePagedRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.keyword = this.keyword;

        // this._rolesService
        //     .getAll(request.keyword, request.skipCount, request.maxResultCount)
        //     .pipe(
        //         finalize(() => {
        //             finishedCallback();
        //         })
        //     )
        //     .subscribe((result: RoleDtoPagedResultDto) => {
        //         this.roles = result.items;
        //         this.showPaging(result, pageNumber);
        //     });
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
        // this.showCreateOrEditRoleDialog();
    }

    editScene(scene: ViewSceneDto): void {
        // this.showCreateOrEditRoleDialog(role.id);
    }

    showCreateOrEditSceneDialog(id?: number): void {
        // let createOrEditRoleDialog: BsModalRef;
        // if (!id) {
        //     createOrEditRoleDialog = this._modalService.show(
        //         CreateRoleDialogComponent,
        //         {
        //             class: 'modal-lg',
        //         }
        //     );
        // } else {
        //     createOrEditRoleDialog = this._modalService.show(
        //         EditRoleDialogComponent,
        //         {
        //             class: 'modal-lg',
        //             initialState: {
        //                 id: id,
        //             },
        //         }
        //     );
        // }
        //
        // createOrEditRoleDialog.content.onSave.subscribe(() => {
        //     this.refresh();
        // });
    }
}
