import {BrowserModule} from "@angular/platform-browser";
import {NgModule} from "@angular/core";
import {AppRoutingModule} from "./app-routing.module";
import {AppComponent} from "./app.component";
import {IonicModule, IonicRouteStrategy} from "@ionic/angular";
import {HttpClientModule} from "@angular/common/http";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {ApiClientModule, AuthenticationClient, BcCommonModule, BookingClient, MasterListClient} from "bc-common";
import {StatusBar} from "@ionic-native/status-bar/ngx";
import {SplashScreen} from "@ionic-native/splash-screen/ngx";
import {environment} from "../environments/environment";
import {RouteReuseStrategy} from "@angular/router";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    IonicModule.forRoot(),
    BrowserModule,
    AppRoutingModule,
    BcCommonModule,
    HttpClientModule,
    ApiClientModule,
    BrowserAnimationsModule

  ],
  providers: [
    StatusBar,
    SplashScreen,
    MasterListClient.MasterListClient,
    AuthenticationClient.AuthenticationClient,
    BookingClient.BookingClient,
    {provide: MasterListClient.API_BASE_URL, useValue: environment.apiUrl},
    {provide: AuthenticationClient.API_BASE_URL, useValue: environment.apiUrl},
    {provide: BookingClient.API_BASE_URL, useValue: environment.apiUrl},
    {provide: RouteReuseStrategy, useClass: IonicRouteStrategy}
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
