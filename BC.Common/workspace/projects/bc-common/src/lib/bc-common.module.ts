import {NgModule} from '@angular/core';
import {BcCommonComponent} from './bc-common.component';
import {IonicModule} from "@ionic/angular";
import {ApiClientModule} from "./api-client/api-client.module";


@NgModule({
  declarations: [BcCommonComponent],
  imports: [
    IonicModule,
    ApiClientModule
  ],
  exports: [BcCommonComponent]
})
export class BcCommonModule {
}
