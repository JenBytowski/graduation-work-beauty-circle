import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationComponent } from './authentication/authentication/authentication.component';
import { DayComponent } from './calendar/day/day.component';
import { WeekComponent } from './calendar/week/week.component';
import { TokenGuard } from 'bc-common';
import { UpdateMasterComponent } from './master-profile/update-master/update-master.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'update',
    pathMatch: 'full',
  },
  {
    path: 'authentication',
    component: AuthenticationComponent,
    pathMatch: 'full',
  },
  {
    path: 'day/:id',
    component: DayComponent,
    pathMatch: 'full',
    canActivate: [TokenGuard],
  },
  {
    path: 'week',
    component: WeekComponent,
    pathMatch: 'full',
    canActivate: [TokenGuard],
  },
  {
    path: 'update',
    component: UpdateMasterComponent,
    pathMatch: 'full',
    canActivate: [TokenGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
