import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomePage} from './home.page';
import {IonComponent} from "@bc-common/ion/ion.component";

const routes: Routes = [
  {
    path: '',
    component: HomePage,
  },
  {
    path: 'ion',
    component: IonComponent,
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomePageRoutingModule {
}
