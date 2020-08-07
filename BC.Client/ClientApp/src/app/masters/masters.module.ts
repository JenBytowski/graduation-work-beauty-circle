import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {IonicModule, IonicRouteStrategy} from '@ionic/angular';
import {SplashScreen} from '@ionic-native/splash-screen/ngx';
import {StatusBar} from '@ionic-native/status-bar/ngx';

import {MastersRoutingModule} from './masters-routing.module';
import {MastersListComponent} from './masters-list/masters-list.component';
import {RouteReuseStrategy} from "@angular/router";
import {MasterProfileComponent} from './master-profile/master-profile.component';


@NgModule({
  declarations: [MastersListComponent, MasterProfileComponent],
  exports: [
    MastersListComponent
  ],
  imports: [
    IonicModule.forRoot(),
    CommonModule,
    MastersRoutingModule
  ],
  providers: [
    StatusBar,
    SplashScreen,
    {provide: RouteReuseStrategy, useClass: IonicRouteStrategy}
  ],
})
export class MastersModule {
}
