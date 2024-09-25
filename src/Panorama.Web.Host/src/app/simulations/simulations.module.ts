import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SimulationsComponent } from './simulations.component';
import {SimulationsRoutingModule} from "@app/simulations/simulations-routing.module";



@NgModule({
  declarations: [
    SimulationsComponent
  ],
  imports: [
    CommonModule,
    SimulationsRoutingModule,
  ]
})
export class SimulationsModule { }
