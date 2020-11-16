import { Component } from '@angular/core';
import {TokenStoreService} from "bc-common";

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {
  constructor(private tokenStore: TokenStoreService) {}
  logOut(){
    this.tokenStore.get() ? this.tokenStore.clear() : {};
  }
}
