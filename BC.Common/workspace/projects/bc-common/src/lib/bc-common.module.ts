import { NgModule } from '@angular/core';
import { BcCommonComponent } from './bc-common.component';
import { IonicModule } from '@ionic/angular';
import { ApiClientModule } from './api-client/api-client.module';
import { MasterListClient } from './api-client/master-list/clients';
import { AuthenticationClient } from './api-client/authentication/clients';
import { BookingClient } from './api-client/booking/clients';

@NgModule({
  declarations: [BcCommonComponent],
  imports: [IonicModule, ApiClientModule],
  providers: [MasterListClient, AuthenticationClient, BookingClient],
  exports: [BcCommonComponent],
})
export class BcCommonModule {}
