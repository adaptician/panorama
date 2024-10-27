import {Component, Injector} from '@angular/core';
import {appModuleAnimation} from "@shared/animations/routerTransition";
import {PagedListingComponentBase, PagedRequestDto} from "@shared/paged-listing-component-base";
import {
    SimulationServiceProxy, ViewSimulationDto, ViewSimulationDtoPagedResultDto,
} from "@shared/service-proxies/service-proxies";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {finalize} from "rxjs/operators";
import {CreateSimulationDialogComponent} from "@app/simulations/create-simulation/create-simulation-dialog.component";
import {PanoTreeNode} from "@shared/service-proxies/common/trees/PanoTreeNode";
import {TreeNode} from "primeng/api/treenode";
import {EditSimulationDialogComponent} from "@app/simulations/edit-simulation/edit-simulation-dialog.component";


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
export class SimulationsComponent extends PagedListingComponentBase<ViewSimulationDto> {
    
    simulations: ViewSimulationDto[] = [];
    simulationNodes: SimulationTreeNode[] = [];
    files!: TreeNode[];
    
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

    editSimulation(simulation: SimulationTreeNode): void {
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

    delete(entity: ViewSimulationDto): void {
        throw new Error('Method not implemented.');
    }

    clearFilters(): void {
        this.keyword = '';
        this.hasRunning = undefined;
        this.getDataPage(1);
    }
    
    private mapTreeNodes(sims: ViewSimulationDto[]): SimulationTreeNode[] {
        if (!sims) return [];
        
        return sims.map(_ => {
            return new SimulationTreeNode(_);
        }, []);
    }
}

class SimulationTreeNode extends PanoTreeNode<ViewSimulationDto> {
    id?: number;
    
    constructor(data: ViewSimulationDto) {
        super();
        
        if (data) {
            // base
            this.label = data.name;
            this.data = data;
            
            // concrete
            this.id = data.id;
        }
    }
}
