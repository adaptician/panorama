import {Component, Injector, OnInit} from '@angular/core';
import {appModuleAnimation} from "@shared/animations/routerTransition";
import {PagedListingComponentBase, PagedRequestDto} from "@shared/paged-listing-component-base";
import {
    SimulationRunServiceProxy,
    SimulationServiceProxy, ViewSimulationDto, ViewSimulationDtoPagedResultDto,
} from "@shared/service-proxies/service-proxies";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {finalize} from "rxjs/operators";
import {CreateSimulationDialogComponent} from "@app/simulations/create-simulation/create-simulation-dialog.component";
import {EditSimulationDialogComponent} from "@app/simulations/edit-simulation/edit-simulation-dialog.component";
import {
    SimulationRunTreeNode,
    SimulationTreeNode
} from "@shared/service-proxies/common/trees/simulation-tree-node";
import {TreeNodeExpandEvent} from "primeng/tree";
import {TreeTableLazyLoadEvent} from "primeng/treetable";


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
export class SimulationsComponent extends PagedListingComponentBase<ViewSimulationDto> implements OnInit {
    
    simulations: ViewSimulationDto[] = [];
    simulationNodes: SimulationTreeNode[] = [];
    
    keyword = '';
    hasRunning: boolean | null;
    advancedFiltersVisible = false;

    constructor(
        injector: Injector,
        private _simulationService: SimulationServiceProxy,
        private _simulationRunService: SimulationRunServiceProxy,
        private _modalService: BsModalService
    ) {
        super(injector);
    }

    ngOnInit() {
    }
    
    list(
        request: PagedSimulationsRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.keyword = this.keyword;
        request.hasRunning = this.hasRunning;

        this._simulationService
            .getAllSimulations(
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
            .subscribe((result: ViewSimulationDtoPagedResultDto) => {
                this.simulations = result.items;
                this.showPaging(result, pageNumber);
                
                this.simulationNodes = this.mapTreeNodes(this.simulations);
            });
    }

    createSimulation(): void {
        this.showCreateOrEditSimulationDialog();
    }

    editSimulation(simulation: ViewSimulationDto): void {
        if (!simulation || simulation.id <= 0) return;
        
        this.showCreateOrEditSimulationDialog(simulation.id);
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
                        simulationId: id,
                    },
                }
            );
        }

        createOrEditSimulationDialog.content.onSave.subscribe(() => {
            this.refresh();
        });
    }

    delete(simulation: ViewSimulationDto): void {
        if (!simulation) return;
        
        abp.message.confirm(
            this.l('SceneDeleteWarningMessage', simulation.name),
            undefined,
            (result: boolean) => {
                if (result) {
                    this.setBusy('saving', true);

                    this._simulationService
                        .deleteSimulation(simulation.id)
                        .pipe(finalize(() => this.setBusy('saving', false)))
                        .subscribe(() => {
                            abp.notify.success(this.l('SuccessfullyDeleted'));
                            this.refresh();
                        });
                }
            }
        );
    }

    clearFilters(): void {
        this.keyword = '';
        this.hasRunning = undefined;
        this.getDataPage(1);
    }

    onPageChange($event: TreeTableLazyLoadEvent) {
        
        this.pageNumber = ($event.first + $event.rows) / this.pageSize;

        this.getDataPage(this.pageNumber);
    }
    
    onNodeExpand(event: TreeNodeExpandEvent) {
        if (!event?.node?.data) return;

        this.setBusy('tree', true);

        const node = event.node;

        this._simulationRunService
            .getAllSimulationRuns(node.data.id)
            .pipe(finalize(() => this.setBusy('tree', false)))
            .subscribe(result => {
                node.children = result.map(_ => new SimulationRunTreeNode(_), []);
                
                this.simulationNodes = [...this.simulationNodes];
            });
    }
    
    private mapTreeNodes(sims: ViewSimulationDto[]): SimulationTreeNode[] {
        if (!sims) return [];
        
        return sims.map(_ => {
            return new SimulationTreeNode(_);
        }, []);
    }

}
