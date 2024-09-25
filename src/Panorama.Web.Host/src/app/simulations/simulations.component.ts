import {Component, Injector} from '@angular/core';
import {AppComponentBase} from "@shared/app-component-base";


@Component({
    selector: 'sim-simulations',
    templateUrl: './simulations.component.html',
    styleUrl: './simulations.component.less'
})
export class SimulationsComponent extends AppComponentBase {

    constructor(injector: Injector) {
        super(injector);
    }

}
