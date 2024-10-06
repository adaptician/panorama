import {Component, EventEmitter, Injector, NgZone, OnInit, Output} from '@angular/core';
import {AppComponentBase} from "@shared/app-component-base";
import {BsModalRef} from "ngx-bootstrap/modal";
import {SceneServiceProxy} from "@shared/service-proxies/service-proxies";
import {AppEvents} from "@shared/AppEvents";
import {SceneReceivedEventData} from "@shared/service-proxies/scenography/events/SceneReceivedEventData";
import {ViewSceneDto} from "@shared/service-proxies/scenography/dtos/ViewSceneDto";
import {finalize} from "rxjs/operators";

@Component({
  selector: 'sim-edit-simulation-dialog',
  templateUrl: './edit-simulation-dialog.component.html',
  styleUrl: './edit-simulation-dialog.component.less'
})
export class EditSimulationDialogComponent extends AppComponentBase implements OnInit {

  private _sceneCorrelationId: string;
  set sceneCorrelationId(value: string) {
    if (value) {
      this._sceneCorrelationId = value;
      
      this.getScene(this._sceneCorrelationId);
    }
  }
  get sceneCorrelationId(): string {
    return this._sceneCorrelationId;
  }
  
  // scene: UpdateSceneDto = new UpdateSceneDto();
  scene: ViewSceneDto = new ViewSceneDto();

  @Output() onSave = new EventEmitter<any>();

  constructor(
      injector: Injector,
      private _sceneService: SceneServiceProxy,
      public bsModalRef: BsModalRef,
      private _zone: NgZone
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.subscribeToEvents();
  }
  
  getScene(correlationId: string): void {
    this.setBusy('loading', true);

    this._sceneService
        .commandGetById(correlationId)
        .pipe(finalize(() => this.setBusy('loading', false)))
        .subscribe(() => {});
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

  private subscribeToEvents(): void {

    this.subscribeToEvent(AppEvents.SignalR_AppEvents_Scene_Received_Trigger,
        (json) => {
console.log(`SCENE RECEIVED ${json}`);
          const data = new SceneReceivedEventData();
          Object.assign(data, JSON.parse(json));

          this._zone.run(() => {
            this.handleSceneReceived(data);
          });
        });

  }

  private handleSceneReceived(data: SceneReceivedEventData): void {

    if (data?.data) {
      const result = data.data;

      this.scene = result;
      this.setBusy('loading', false);
    }
  }
}
