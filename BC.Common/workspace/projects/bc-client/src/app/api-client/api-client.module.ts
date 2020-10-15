import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from "@angular/common/http";
import {API_BASE_URL as API_BASE_URL_MASTER_LIST, MasterListClient} from "./master-list/clients";
import {API_BASE_URL as API_BASE_URL_AUTHENTICATION, AuthenticationClient} from "./authentication/clients";
import {environment} from "../../environments/environment";


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  providers: [
    MasterListClient,
    AuthenticationClient,
    {provide: API_BASE_URL_MASTER_LIST, useValue: environment.apiUrl},
    {provide: API_BASE_URL_AUTHENTICATION, useValue: environment.apiUrl}
  ],
})
export class ApiClientModule {
}
