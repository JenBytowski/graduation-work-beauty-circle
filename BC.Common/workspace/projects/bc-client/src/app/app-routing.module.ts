import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AuthenticationComponent} from "./authentication/authentication/authentication.component";
import {MastersListComponent} from "./masters/masters-list/masters-list.component";
import {MasterProfileComponent} from "./masters/master-profile/master-profile.component";
import {HomePage} from "./home/home.page";
import {TokenStoreService} from "bc-common";

const tokenStoreService: TokenStoreService = new TokenStoreService();
const routes: Routes = [
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
    pathMatch: 'full'
  },
  {
    path: 'master/:id',
    component: MasterProfileComponent,
    pathMatch: 'full'
  },
  {
    path: '',
    redirectTo: tokenStoreService.get() ? 'masters' : 'authentication',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
