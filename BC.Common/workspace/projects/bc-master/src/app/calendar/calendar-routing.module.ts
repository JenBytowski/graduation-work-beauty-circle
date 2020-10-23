import {NgModule} from "@angular/core";
import {RouterModule, Routes} from "@angular/router";
import {DayComponent} from "./day/day.component";
import {WeekComponent} from "./week/week.component";

const routes: Routes = [
  {
    path: '',
    redirectTo: 'week',
    pathMatch: 'full'
  },
  {
    path: 'day/:id',
    component: DayComponent,
    pathMatch: 'full'
  },
  {
    path: 'week',
    component: WeekComponent,
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CalendarRoutingModule {
}
