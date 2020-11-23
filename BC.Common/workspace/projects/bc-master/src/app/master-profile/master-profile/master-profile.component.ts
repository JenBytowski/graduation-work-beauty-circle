import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {MasterListClient, TokenStoreService} from "bc-common";
import * as jwt_decode from "jwt-decode";


@Component({
  selector: 'app-master-profile',
  templateUrl: './master-profile.component.html',
  styleUrls: ['./master-profile.component.scss']
})
export class MasterProfileComponent implements OnInit {

  public vm: Vm = new Vm();

  constructor(private route: ActivatedRoute, private masterList: MasterListClient.MasterListClient, private tokenStore: TokenStoreService) {
  }

  ngOnInit(): void {
    //const id = this.route.snapshot.paramMap.get('id');
    console.log(this.tokenStore.get());
    console.log(jwt_decode.default(this.tokenStore.get()));//
    this.masterList.getMasterById(jwt_decode.default(this.tokenStore.get())['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']).subscribe(data => {
      if(data){
        (data as any).starRating = this.countStarRating(data.averageRating);
        this.vm.Master = data;
        console.log(data);//
      }
    });
  }

  public countStarRating(rating: number) {
    rating = Math.round(rating * 2) / 2;
    return [
      (rating >= 1 ? 'star' : rating >= 0.5 ? 'star-half-outline' : 'star-outline'),
      (rating >= 2 ? 'star' : rating >= 1.5 ? 'star-half-outline' : 'star-outline'),
      (rating >= 3 ? 'star' : rating >= 2.5 ? 'star-half-outline' : 'star-outline'),
      (rating >= 4 ? 'star' : rating >= 3.5 ? 'star-half-outline' : 'star-outline'),
      (rating >= 5 ? 'star' : rating >= 4.5 ? 'star-half-outline' : 'star-outline'),
    ];
  }
}

class Vm {
  public Master: any;
}
