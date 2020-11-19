import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AuthenticationComponent} from "./authentication/authentication/authentication.component";
import {MastersListComponent} from "./masters/masters-list/masters-list.component";
import {MasterProfileComponent} from "./masters/master-profile/master-profile.component";
import {HomePage} from "./home/home.page";
import {TokenGuard} from "bc-common";
import {NotFoundPageComponent} from "./exeption/not-found-page/not-found-page.component";
import {UnauthorizedPageComponent} from "./exeption/unauthorized-page/unauthorized-page.component";

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    component: HomePage,
    pathMatch: 'full'
  },
  {
    path: 'authentication',
    component: AuthenticationComponent,
    pathMatch: 'full'
  },
  {
    path: 'masters',
    component: MastersListComponent,
    pathMatch: 'full',
    canActivate: [TokenGuard]
  },
  {
    path: 'master/:id',
    component: MasterProfileComponent,
    pathMatch: 'full',
    canActivate: [TokenGuard]
  },
  {
    path: 'unauthorized',
    component: UnauthorizedPageComponent,
    pathMatch: 'full'
  },
  {
    path: '**',
    component: NotFoundPageComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
