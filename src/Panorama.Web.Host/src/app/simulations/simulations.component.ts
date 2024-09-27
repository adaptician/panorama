import {Component, Injector} from '@angular/core';
import {appModuleAnimation} from "@shared/animations/routerTransition";


@Component({
    selector: 'sim-simulations',
    templateUrl: './simulations.component.html',
    styleUrl: './simulations.component.less',
    animations: [appModuleAnimation()]
})
export class SimulationsComponent {

    
}
