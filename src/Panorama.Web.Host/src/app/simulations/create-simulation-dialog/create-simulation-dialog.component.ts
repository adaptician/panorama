import {Component, EventEmitter, Injector, OnInit, Output} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import {AppComponentBase} from "@shared/app-component-base";
import {SceneServiceProxy} from "@shared/service-proxies/scenography/scenography.service-proxies";
import {CreateSceneDto} from "@shared/service-proxies/scenography/dtos/CreateSceneDto";
import {finalize} from "rxjs/operators";

@Component({
    selector: 'sim-create-simulation-dialog',
    templateUrl: './create-simulation-dialog.component.html',
    styleUrl: './create-simulation-dialog.component.less'
})
export class CreateSimulationDialogComponent extends AppComponentBase implements OnInit {
    
    scene: CreateSceneDto = new CreateSceneDto();
    
    @Output() onSave = new EventEmitter<any>();

    constructor(
        injector: Injector,
        private _sceneService: SceneServiceProxy,
        public bsModalRef: BsModalRef
    ) {
        super(injector);
    }

    ngOnInit(): void {
        
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
