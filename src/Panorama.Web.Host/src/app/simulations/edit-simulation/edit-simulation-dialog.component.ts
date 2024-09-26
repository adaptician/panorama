import {Component, EventEmitter, Injector, Output} from '@angular/core';
import {AppComponentBase} from "@shared/app-component-base";
import {SceneServiceProxy} from "@shared/service-proxies/scenography/scenography.service-proxies";
import {BsModalRef} from "ngx-bootstrap/modal";
import {UpdateSceneDto} from "@shared/service-proxies/scenography/dtos/UpdateSceneDto";
import {finalize} from "rxjs/operators";

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
  
  scene: UpdateSceneDto = new UpdateSceneDto();

  @Output() onSave = new EventEmitter<any>();

  constructor(
      injector: Injector,
      private _sceneService: SceneServiceProxy,
      public bsModalRef: BsModalRef
  ) {
    super(injector);
  }
  
  getScene(id: number): void {
    this.setBusy('load', true);

    this._sceneService
        .get(id)
        .pipe(finalize(() => this.setBusy('load', false)))
        .subscribe(
            (result) => {
              this.scene = this.mapper.map(result, UpdateSceneDto);
            }
        );
  }

  save(): void {
    this.setBusy('saving', true);

    this._sceneService
        .update(this.scene)
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
