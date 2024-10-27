import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CreateSceneDialogComponent} from "./create-scene/create-scene-dialog.component";
import {ScenesComponent} from "./scenes.component";
import {ScenesRoutingModule} from "./scenes-routing.module";
import {FormsModule} from "@angular/forms";
import {NgxPaginationModule} from "ngx-pagination";
import {TabsModule} from "ngx-bootstrap/tabs";
import {SharedModule} from "@shared/shared.module";
import {EditSceneDialogComponent} from "@app/scenes/edit-scene/edit-scene-dialog.component";


@NgModule({
    declarations: [
        CreateSceneDialogComponent,
        EditSceneDialogComponent,
        ScenesComponent
    ],
    imports: [
        CommonModule,
        ScenesRoutingModule,
        FormsModule,
        NgxPaginationModule,
        TabsModule,
        SharedModule,
        TabsModule,
    ]
})
export class ScenesModule { }
