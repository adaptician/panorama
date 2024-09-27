import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SimulationsComponent } from './simulations.component';
import {SimulationsRoutingModule} from "@app/simulations/simulations-routing.module";
import {FormsModule} from "@angular/forms";
import {NgxPaginationModule} from "@node_modules/ngx-pagination";
import {SharedModule} from "@shared/shared.module";
import { CreateSimulationDialogComponent } from './create-simulation/create-simulation-dialog.component';
import {TabsModule} from "ngx-bootstrap/tabs";
import { EditSimulationDialogComponent } from './edit-simulation/edit-simulation-dialog.component';
import { ScenesComponent } from './scenes/scenes.component';



@NgModule({
  declarations: [
    SimulationsComponent,
    CreateSimulationDialogComponent,
    EditSimulationDialogComponent,
    ScenesComponent
  ],
  imports: [
    CommonModule,
    SimulationsRoutingModule,
    FormsModule,
    NgxPaginationModule,
    TabsModule,
    SharedModule,
    TabsModule,
  ]
})
export class SimulationsModule { }
