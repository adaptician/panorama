import {Component, Injector} from '@angular/core';
import {appModuleAnimation} from "@shared/animations/routerTransition";
import {PagedListingComponentBase, PagedRequestDto} from "@shared/paged-listing-component-base";
import {
    GetSimulationDto, GetSimulationDtoPagedResultDto,
    SimulationServiceProxy,
} from "@shared/service-proxies/service-proxies";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {finalize} from "rxjs/operators";
import {CreateSimulationDialogComponent} from "@app/simulations/create-simulation/create-simulation-dialog.component";

class PagedSimulationsRequestDto extends PagedRequestDto {
    keyword: string;
    hasRunning: boolean | null;
}

@Component({
    selector: 'sim-simulations',
    templateUrl: './simulations.component.html',
    styleUrl: './simulations.component.less',
    animations: [appModuleAnimation()]
})
export class SimulationsComponent extends PagedListingComponentBase<GetSimulationDto> {
    
    simulations: GetSimulationDto[] = [];
    keyword = '';
    hasRunning: boolean | null;
    advancedFiltersVisible = false;

    constructor(
        injector: Injector,
        private _simulationService: SimulationServiceProxy,
        private _modalService: BsModalService
    ) {
        super(injector);
    }

    list(
        request: PagedSimulationsRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.keyword = this.keyword;
        request.hasRunning = this.hasRunning;

        this._simulationService
            .getAll(
                request.keyword,
                request.hasRunning,
                request.skipCount,
                request.maxResultCount
            )
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: GetSimulationDtoPagedResultDto) => {
                this.simulations = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    createSimulation(): void {
        this.showCreateOrEditSimulationDialog();
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
        }// else {
        //     createOrEditTenantDialog = this._modalService.show(
        //         EditTenantDialogComponent,
        //         {
        //             class: 'modal-lg',
        //             initialState: {
        //                 id: id,
        //             },
        //         }
        //     );
        // }

        createOrEditSimulationDialog.content.onSave.subscribe(() => {
            this.refresh();
        });
    }

    delete(entity: GetSimulationDto): void {
        throw new Error('Method not implemented.');
    }

    clearFilters(): void {
        this.keyword = '';
        this.hasRunning = undefined;
        this.getDataPage(1);
    }
}
