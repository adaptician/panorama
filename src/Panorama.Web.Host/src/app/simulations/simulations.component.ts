import {Component, Injector} from '@angular/core';
import {appModuleAnimation} from "@shared/animations/routerTransition";
import {PagedListingComponentBase, PagedRequestDto} from "@shared/paged-listing-component-base";
import {
    SimulationRunServiceProxy,
    SimulationServiceProxy, ViewSimulationDto, ViewSimulationDtoPagedResultDto, ViewSimulationRunDto,
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
        private _simulationRunService: SimulationRunServiceProxy,
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

    nodeExpand(event: SimulationTreeNode) {
        
        if (event) {
            this.setBusy('loading', true);

            // this._simulationRunService
            //     .getAllSimulationRuns(event.id)
            //     .pipe(finalize(() => this.setBusy('loading', false)))
            //     .subscribe(result => {
            //         // TODO:T FIX MAPPING
            //         event.children = result.map(_ => new PanoTreeNode<ViewSimulationRunDto>(_));
            //     });
        }
        
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
    children?: PanoTreeNode<ViewSimulationRunDto>[];
    
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
