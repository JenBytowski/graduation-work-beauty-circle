import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  ApiClientModule,
  AuthenticationClient, AuthInterceptorService,
  BcCommonModule,
  BookingClient,
  MasterListClient, TokenGuard,
} from 'bc-common';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { environment } from '../environments/environment';
import { RouteReuseStrategy } from '@angular/router';
import { CalendarModule } from './calendar/calendar.module';
import { AuthenticationModule } from './authentication/authentication.module';
import { MasterProfileModule } from './master-profile/master-profile.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    IonicModule.forRoot(),
    BrowserModule,
    AppRoutingModule,
    BcCommonModule,
    HttpClientModule,
    ApiClientModule,
    BrowserAnimationsModule,
    CalendarModule,
    AuthenticationModule,
    MasterProfileModule,
  ],
  providers: [
    StatusBar,
    SplashScreen,
    TokenGuard,
    MasterListClient.MasterListClient,
    AuthenticationClient.AuthenticationClient,
    BookingClient.BookingClient,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true,
    },
    { provide: MasterListClient.API_BASE_URL, useValue: environment.apiUrl },
    {
      provide: AuthenticationClient.API_BASE_URL,
      useValue: environment.apiUrl,
    },
    { provide: BookingClient.API_BASE_URL, useValue: environment.apiUrl },
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
