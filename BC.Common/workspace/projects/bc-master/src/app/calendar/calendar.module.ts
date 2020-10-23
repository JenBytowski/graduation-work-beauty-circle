import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CalendarRoutingModule } from './calendar-routing.module';
import { DayComponent } from './day/day.component';
import { WeekComponent } from './week/week.component';
import {IonicModule} from "@ionic/angular";


@NgModule({
  declarations: [DayComponent, WeekComponent],
  imports: [
    IonicModule,
    CommonModule,
    CalendarRoutingModule
  ]
})
export class CalendarModule { }
