import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthenticationRoutingModule } from './authentication-routing.module';
import {AuthenticationComponent} from "./authentication/authentication.component";
import {IonicModule} from "@ionic/angular";
import {CookieService} from "ngx-cookie-service";


@NgModule({
  declarations: [AuthenticationComponent],
  imports: [
    CommonModule,
    AuthenticationRoutingModule,
    IonicModule
  ],
  providers: [CookieService]
})
export class AuthenticationModule { }
