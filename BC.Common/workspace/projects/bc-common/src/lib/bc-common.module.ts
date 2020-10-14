import {NgModule} from '@angular/core';
import {BcCommonComponent} from './bc-common.component';
import {IonComponent} from './ion/ion.component';
import {IonicModule} from "@ionic/angular";


@NgModule({
  declarations: [BcCommonComponent, IonComponent],
  imports: [
    IonicModule
  ],
  exports: [BcCommonComponent]
})
export class BcCommonModule {
}
