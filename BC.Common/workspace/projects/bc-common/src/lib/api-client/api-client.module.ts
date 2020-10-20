import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from "@angular/common/http";
import {MasterListClient} from "./master-list/clients";
import {AuthenticationClient} from "./authentication/clients";
import {BookingClient} from "./booking/clients";


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  providers: [
    MasterListClient,
    AuthenticationClient,
    BookingClient
  ],
})
export class ApiClientModule {
}
