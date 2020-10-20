import {NgModule} from "@angular/core";
import {BrowserModule} from "@angular/platform-browser";
import {AppRoutingModule} from "./app-routing.module";
import {AppComponent} from "./app.component";
import {BcCommonModule} from "bc-common";
import {IonicModule, IonicRouteStrategy} from "@ionic/angular";
import {HttpClientModule} from "@angular/common/http";
import {MastersModule} from "./masters/masters.module";
import {AuthenticationModule} from "./authentication/authentication.module";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {HomePageModule} from "./home/home.module";
import {StatusBar} from "@ionic-native/status-bar/ngx";
import {SplashScreen} from "@ionic-native/splash-screen/ngx";
import {RouteReuseStrategy} from "@angular/router";
import {API_BASE_URL as API_BASE_URL_MASTER_LIST, MasterListClient} from "../../../bc-common/src/lib/api-client/master-list/clients";
import {API_BASE_URL as API_BASE_URL_AUTHENTICATION, } from "bc-common";
import {API_BASE_URL as API_BASE_URL_BOOKING, BookingClient} from "./booking/clients";
import {environment} from "../environments/environment";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BcCommonModule,
    IonicModule,
    MastersModule,
    HttpClientModule,
    AuthenticationModule,
    BrowserAnimationsModule,
    HomePageModule,


  ],
  providers: [
    StatusBar,
    SplashScreen,
    ///
    MasterListClient,
    AuthenticationClient,
    BookingClient,
    {provide: API_BASE_URL_MASTER_LIST, useValue: environment.apiUrl},
    {provide: API_BASE_URL_AUTHENTICATION, useValue: environment.apiUrl},
    {provide: API_BASE_URL_BOOKING, useValue: environment.apiUrl},
    ///
    {provide: RouteReuseStrategy, useClass: IonicRouteStrategy}
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
