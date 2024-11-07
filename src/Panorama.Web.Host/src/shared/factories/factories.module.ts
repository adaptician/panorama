import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CameraFactory} from "./camera.factory";
import {MeshFactory} from "@shared/factories/mesh.factory";


@NgModule({
    declarations: [],
    imports: [
        CommonModule
    ],
    providers: [
        CameraFactory,
        MeshFactory,
    ]
})
export class FactoriesModule {
}
