import {Component, EventEmitter, Injector, Output} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import {AppComponentBase} from "@shared/app-component-base";
import {finalize} from "rxjs/operators";
import {CreateSceneDto, SceneServiceProxy} from "@shared/service-proxies/service-proxies";

@Component({
    selector: 'sim-create-simulation-dialog',
    templateUrl: './create-simulation-dialog.component.html',
    styleUrl: './create-simulation-dialog.component.less'
})
export class CreateSimulationDialogComponent extends AppComponentBase {
    
    scene: CreateSceneDto = new CreateSceneDto();
    
    @Output() onSave = new EventEmitter<any>();

    constructor(
        injector: Injector,
        private _sceneService: SceneServiceProxy,
        public bsModalRef: BsModalRef
    ) {
        super(injector);
    }

    save(): void {
        this.setBusy('saving', true);
        
        this._sceneService
            .create(this.scene)
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
