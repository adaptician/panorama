import {Component, EventEmitter, Injector, Output} from '@angular/core';
import {AppComponentBase} from "@shared/app-component-base";
import {BsModalRef} from "ngx-bootstrap/modal";
import {finalize} from "rxjs/operators";
import {SceneServiceProxy} from "@shared/service-proxies/service-proxies";

@Component({
  selector: 'sim-edit-simulation-dialog',
  templateUrl: './edit-simulation-dialog.component.html',
  styleUrl: './edit-simulation-dialog.component.less'
})
export class EditSimulationDialogComponent extends AppComponentBase {

  private _sceneId: number;
  set sceneId(value: number) {
    if (value) {
      this._sceneId = value;
      
      this.getScene(this._sceneId);
    }
  }
  get sceneId(): number {
    return this._sceneId;
  }
  
  // scene: UpdateSceneDto = new UpdateSceneDto();

  @Output() onSave = new EventEmitter<any>();

  constructor(
      injector: Injector,
      private _sceneService: SceneServiceProxy,
      public bsModalRef: BsModalRef
  ) {
    super(injector);
  }
  
  getScene(id: number): void {
    this.setBusy('loading', true);

    // this._sceneService
    //     .getById(id)
    //     .pipe(finalize(() => this.setBusy('loading', false)))
    //     .subscribe(
    //         (result) => {
    //           this.scene = result;
    //         }
    //     );
  }

  save(): void {
    this.setBusy('saving', true);

    // this._sceneService
    //     .update(this.scene)
    //     .pipe(finalize(() => this.setBusy('saving', false)))
    //     .subscribe(
    //         () => {
    //           this.notify.info(this.l('SavedSuccessfully'));
    //           this.bsModalRef.hide();
    //           this.onSave.emit();
    //         }
    //     );
  }
}
