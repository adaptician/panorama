import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {appModuleAnimation} from "@shared/animations/routerTransition";

@Component({
  selector: 'sim-simulator',
  templateUrl: './simulator.component.html',
  styleUrl: './simulator.component.less',
  animations: [appModuleAnimation()]
})
export class SimulatorComponent {

}
