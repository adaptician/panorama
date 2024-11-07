import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {PrototypeRegistry} from "./prototype.registry";



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    PrototypeRegistry,
  ]
})
export class RegistriesModule { }
