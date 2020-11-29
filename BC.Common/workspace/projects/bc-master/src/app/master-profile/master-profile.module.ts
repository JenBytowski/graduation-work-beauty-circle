import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { MasterProfileRoutingModule } from './master-profile-routing.module';
import { UpdateMasterComponent } from './update-master/update-master.component';


@NgModule({
  declarations: [UpdateMasterComponent],
  imports: [
    CommonModule,
    IonicModule,
    MasterProfileRoutingModule
  ]
})
export class MasterProfileModule { }
