import {Component, EventEmitter, Injector, Output} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import {AppComponentBase} from "@shared/app-component-base";
import {finalize} from "rxjs/operators";
import {CreateSceneDto, SceneServiceProxy} from "@shared/service-proxies/service-proxies";

@Component({
    selector: 'sim-create-scene-dialog',
    templateUrl: './create-scene-dialog.component.html',
    styleUrl: './create-scene-dialog.component.less'
})
export class CreateSceneDialogComponent extends AppComponentBase {
    
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
            .commandCreate(this.scene)
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
