import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SimulationsComponent } from './simulations.component';
import {SimulationsRoutingModule} from "@app/simulations/simulations-routing.module";
import {FormsModule} from "@angular/forms";
import {NgxPaginationModule} from "@node_modules/ngx-pagination";
import {SharedModule} from "@shared/shared.module";



@NgModule({
  declarations: [
    SimulationsComponent
  ],
  imports: [
    CommonModule,
    SimulationsRoutingModule,
    FormsModule,
    NgxPaginationModule,
    SharedModule,
  ]
})
export class SimulationsModule { }
