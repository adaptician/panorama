import {Component, EventEmitter, Injector, Output} from '@angular/core';
import {SimulationServiceProxy, UpdateSimulationDto} from "@shared/service-proxies/service-proxies";
import {AppComponentBase} from "@shared/app-component-base";
import {BsModalRef} from "ngx-bootstrap/modal";
import {finalize} from "rxjs/operators";

@Component({
  selector: 'sim-edit-simulation',
  templateUrl: './edit-simulation-dialog.component.html',
  styleUrl: './edit-simulation-dialog.component.less'
})
export class EditSimulationDialogComponent extends AppComponentBase {

  @Output() onSave = new EventEmitter<any>();

  private _simulationId: number;
  set simulationId(value: number) {
    if (value) {
      this._simulationId = value;

      this.getSimulation(this._simulationId);
    }
  }
  get simulationId(): number {
    return this._simulationId;
  }
  
  simulation: UpdateSimulationDto = new UpdateSimulationDto();

  constructor(
      injector: Injector,
      private _simulationService: SimulationServiceProxy,
      public bsModalRef: BsModalRef
  ) {
    super(injector);
  }
  
  getSimulation(id: number): void {
    this.setBusy('loading', true);

    this._simulationService
        .getSimulationById(id)
        .pipe(finalize(() => this.setBusy('loading', false)))
        .subscribe(result => {
          this.simulation = result;
        });
  }

  save(): void {
    this.setBusy('saving', true);

    this._simulationService
        .updateSimulation(this.simulation)
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
