import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AuthenticationComponent} from "./authentication/authentication/authentication.component";
import {MastersListComponent} from "./masters/masters-list/masters-list.component";
import {WelcomeComponent} from "./authentication/welcome/welcome.component";
import {MasterProfileComponent} from "./masters/master-profile/master-profile.component";

const routes: Routes = [
  {
    path: 'home',
    loadChildren: () => import('./home/home.module').then(m => m.HomePageModule)
  },
  {
    path: '',
    redirectTo: 'home',
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
    data: {animation: 'isLeft'}
  },
  {
    path: 'master/:id',
    component: MasterProfileComponent,
    pathMatch: 'full',
    data: {animation: 'isRight'}
  },
  {
    path: 'welcome',
    component: WelcomeComponent,
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
