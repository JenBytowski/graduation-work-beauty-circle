import {NgModule} from '@angular/core';
import {CommonModule, DOCUMENT} from '@angular/common';

import {AuthenticationRoutingModule} from './authentication-routing.module';
import {AuthenticationComponent} from "./authentication/authentication.component";
import {IonicModule} from "@ionic/angular";
import {CookieService} from "ngx-cookie-service";
import {WelcomeComponent} from "./welcome/welcome.component";

export function getBaseUrl(document) {
  return document.getElementsByTagName('base')[0].href;
}

@NgModule({
  declarations: [AuthenticationComponent, WelcomeComponent],
  imports: [
    CommonModule,
    AuthenticationRoutingModule,
    IonicModule
  ],
  providers: [
    {provide: 'BASE_URL', useFactory: getBaseUrl, deps: [DOCUMENT]},
    CookieService]
})
export class AuthenticationModule {
}
