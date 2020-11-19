import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MasterProfileRoutingModule} from './master-profile-routing.module';
import {MasterProfileComponent} from './master-profile/master-profile.component';


@NgModule({
  declarations: [MasterProfileComponent],
  imports: [
    CommonModule,
    MasterProfileRoutingModule
  ]
})
export class MasterProfileModule {
}
