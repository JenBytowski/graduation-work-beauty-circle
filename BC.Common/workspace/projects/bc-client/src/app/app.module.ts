import {NgModule} from "@angular/core";
import {BrowserModule} from "@angular/platform-browser";
import {AppRoutingModule} from "./app-routing.module";
import {AppComponent} from "./app.component";
import {
  ApiClientModule,
  AuthenticationClient,
  AuthInterceptorService,
  BcCommonModule,
  BookingClient,
  MasterListClient
} from "bc-common";
import {IonicModule, IonicRouteStrategy} from "@ionic/angular";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {MastersModule} from "./masters/masters.module";
import {AuthenticationModule} from "./authentication/authentication.module";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {HomePageModule} from "./home/home.module";
import {StatusBar} from "@ionic-native/status-bar/ngx";
import {SplashScreen} from "@ionic-native/splash-screen/ngx";
import {RouteReuseStrategy} from "@angular/router";
import {environment} from "../environments/environment";
import {ExeptionModule} from './exeption/exeption.module';
import {TokenGuard} from "./guards/masters.guard";
import {AuthenticationGuard} from "./guards/authentication.guard";

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
    ApiClientModule,
    ExeptionModule
  ],
  providers: [
    StatusBar,
    SplashScreen,
    TokenGuard,
    AuthenticationGuard,
    MasterListClient.MasterListClient,
    AuthenticationClient.AuthenticationClient,
    BookingClient.BookingClient,
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi: true},
    {provide: MasterListClient.API_BASE_URL, useValue: environment.apiUrl},
    {provide: AuthenticationClient.API_BASE_URL, useValue: environment.apiUrl},
    {provide: BookingClient.API_BASE_URL, useValue: environment.apiUrl},
    {provide: RouteReuseStrategy, useClass: IonicRouteStrategy}
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
