import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {MasterListClient} from "bc-common";

@Component({
  selector: 'app-master-profile',
  templateUrl: './master-profile.component.html',
  styleUrls: ['./master-profile.component.css']
})
export class MasterProfileComponent implements OnInit {

  public vm: Vm = new Vm();

  constructor(private route: ActivatedRoute, private masterList: MasterListClient.MasterListClient) {
  }

  ngOnInit(): void {
    //const id = this.route.snapshot.paramMap.get('id');
    this.masterList.getMasterById('D08AC59F-2155-48A5-84A9-59690294591B').subscribe(data => {
      (data as any).starRating = this.countStarRating(data.averageRating);
      this.vm.Master = data;
      console.log(data);
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
