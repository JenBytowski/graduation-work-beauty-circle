import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from "@angular/common/http";
import {API_BASE_URL, MasterListClient} from "./master-list/clients";
import {environment} from "../../environments/environment";
import {AuthenticationClient} from "./authentication/clients";


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  providers: [
    MasterListClient,
    AuthenticationClient,
    {provide: API_BASE_URL, useValue: environment.apiUrl}
  ],
})
export class ApiClientModule {
}
