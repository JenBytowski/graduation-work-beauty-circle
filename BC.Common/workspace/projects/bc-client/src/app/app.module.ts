import {NgModule} from "@angular/core";
import {BrowserModule} from "@angular/platform-browser";
import {AppRoutingModule} from "./app-routing.module";
import {AppComponent} from "./app.component";
import {ApiClientModule, AuthenticationClient, BcCommonModule, BookingClient, MasterListClient} from "bc-common";
import {IonicModule, IonicRouteStrategy} from "@ionic/angular";
import {HttpClientModule} from "@angular/common/http";
import {MastersModule} from "./masters/masters.module";
import {AuthenticationModule} from "./authentication/authentication.module";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {HomePageModule} from "./home/home.module";
import {StatusBar} from "@ionic-native/status-bar/ngx";
import {SplashScreen} from "@ionic-native/splash-screen/ngx";
import {RouteReuseStrategy} from "@angular/router";
import {environment} from "../environments/environment";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    IonicModule.forRoot(),
    BrowserModule,
    AppRoutingModule,
    BcCommonModule,
    MastersModule,
    HttpClientModule,
    AuthenticationModule,
    BrowserAnimationsModule,
    HomePageModule,
    ApiClientModule


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
