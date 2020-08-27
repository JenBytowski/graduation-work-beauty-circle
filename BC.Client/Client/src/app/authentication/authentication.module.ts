import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthenticationRoutingModule } from './authentication-routing.module';
import {AuthenticationComponent} from "./authentication/authentication.component";
import {IonicModule} from "@ionic/angular";


@NgModule({
  declarations: [AuthenticationComponent],
  imports: [
    CommonModule,
    AuthenticationRoutingModule,
    IonicModule
  ]
})
export class AuthenticationModule { }
