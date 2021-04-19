import { Component, OnInit } from '@angular/core';
import {TokenStoreService} from "bc-common";

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {
  public token: string;
  constructor(public tokenStore: TokenStoreService) {}
  logOut(){
    this.tokenStore.get() ? this.tokenStore.clear() : {};
  }
}
