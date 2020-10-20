import {NgModule} from '@angular/core';
import {BcCommonComponent} from './bc-common.component';
import {IonicModule} from "@ionic/angular";


@NgModule({
  declarations: [BcCommonComponent],
  imports: [
    IonicModule.forRoot()
  ],
  exports: [BcCommonComponent]
})
export class BcCommonModule {
}
