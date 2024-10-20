import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SimulationsComponent} from './simulations.component';
import {SimulationsRoutingModule} from "@app/simulations/simulations-routing.module";
import {FormsModule} from "@angular/forms";
import {NgxPaginationModule} from "@node_modules/ngx-pagination";
import {SharedModule} from "@shared/shared.module";
import {TabsModule} from "ngx-bootstrap/tabs";
import {ScenesComponent} from './scenes/scenes.component';
import {CreateSceneDialogComponent} from "@app/simulations/scenes/create-scene/create-scene-dialog.component";
import {EditSceneDialogComponent} from "@app/simulations/scenes/edit-simulation/edit-scene-dialog.component";


@NgModule({
  declarations: [
    SimulationsComponent,
    CreateSceneDialogComponent,
    EditSceneDialogComponent,
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
