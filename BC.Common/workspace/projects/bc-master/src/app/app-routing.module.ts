import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AuthenticationComponent} from "./authentication/authentication/authentication.component";
import {DayComponent} from "./calendar/day/day.component";
import {WeekComponent} from "./calendar/week/week.component";
import {TokenGuard} from "bc-common";
import {MasterProfileComponent} from "./master-profile/master-profile/master-profile.component";

const routes: Routes = [
  {
    path: '',
    redirectTo: 'master',
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
    pathMatch: 'full',
    canActivate: [TokenGuard]
  },
  {
    path: 'week',
    component: WeekComponent,
    pathMatch: 'full',
    canActivate: [TokenGuard]
  },
  {
    path: 'master',
    component: MasterProfileComponent,
    pathMatch: 'full',
    canActivate: [TokenGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
