import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SimulationsComponent} from './simulations.component';
import {SimulationsRoutingModule} from "@app/simulations/simulations-routing.module";
import {FormsModule} from "@angular/forms";
import {SharedModule} from "@shared/shared.module";
import {TabsModule} from "ngx-bootstrap/tabs";
import { CreateSimulationDialogComponent } from './create-simulation/create-simulation-dialog.component';
import {TreeModule} from "primeng/tree";
import { EditSimulationDialogComponent } from './edit-simulation/edit-simulation-dialog.component';
import {NgxPaginationModule} from "ngx-pagination";
import {Button} from "primeng/button";
import {TreeTableModule} from "primeng/treetable";
import {TooltipModule} from "ngx-bootstrap/tooltip";


@NgModule({
  declarations: [
    SimulationsComponent,
    CreateSimulationDialogComponent,
    EditSimulationDialogComponent
  ],
    imports: [
        CommonModule,
        SimulationsRoutingModule,
        FormsModule,
        NgxPaginationModule,
        TabsModule,
        SharedModule,
        TabsModule,
        TreeModule,
        TreeTableModule,
        Button,
        TooltipModule,
    ]
})
export class SimulationsModule { }
