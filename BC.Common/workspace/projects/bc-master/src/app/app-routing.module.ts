import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AuthenticationComponent} from "./authentication/authentication/authentication.component";
import {DayComponent} from "./calendar/day/day.component";
import {WeekComponent} from "./calendar/week/week.component";

const routes: Routes = [
  {
    path: '',
    redirectTo: 'authentication',
    pathMatch: 'full'
  },
  {
    path: 'authentication',
    component: AuthenticationComponent,
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
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
