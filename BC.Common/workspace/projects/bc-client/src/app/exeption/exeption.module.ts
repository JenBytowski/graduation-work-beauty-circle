import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ExeptionRoutingModule } from './exeption-routing.module';
import { NotFoundPageComponent } from './not-found-page/not-found-page.component';
import { UnauthorizedPageComponent } from './unauthorized-page/unauthorized-page.component';
import {IonicModule} from "@ionic/angular";


@NgModule({
  declarations: [NotFoundPageComponent, UnauthorizedPageComponent],
  imports: [
    CommonModule,
    ExeptionRoutingModule,
    IonicModule
  ]
})
export class ExeptionModule { }
