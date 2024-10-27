import {Component, EventEmitter, Injector, Output} from '@angular/core';
import {CreateSimulationDto, SceneServiceProxy, SimulationServiceProxy} from "@shared/service-proxies/service-proxies";
import {AppComponentBase} from "@shared/app-component-base";
import {BsModalRef} from "ngx-bootstrap/modal";
import {finalize} from "rxjs/operators";

@Component({
  selector: 'sim-create-simulation-dialog',
  templateUrl: './create-simulation-dialog.component.html',
  styleUrl: './create-simulation-dialog.component.less'
})
export class CreateSimulationDialogComponent extends AppComponentBase {

  @Output() onSave = new EventEmitter<any>();

  simulation: CreateSimulationDto = new CreateSimulationDto();

  constructor(
      injector: Injector,
      private _simulationService: SimulationServiceProxy,
      public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  save(): void {
    this.setBusy('saving', true);

    this._simulationService
        .createSimulation(this.simulation)
        .pipe(finalize(() => this.setBusy('saving', false)))
        .subscribe(
            () => {
              this.notify.info(this.l('SavedSuccessfully'));
              this.bsModalRef.hide();
              this.onSave.emit();
            }
        );
  }
  
}
