import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from "@angular/common/http";
import {API_BASE_URL, MasterListClient} from "./nswag/clients";


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  providers: [
    MasterListClient,
    {provide: API_BASE_URL, useValue: 'https://localhost:5001'}
  ],
})
export class ApiClientModule {
}
