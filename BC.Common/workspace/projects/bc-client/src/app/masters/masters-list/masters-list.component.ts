import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {MenuController} from '@ionic/angular';
import {MasterListClient} from "bc-common";

@Component({
  selector: 'app-masters-list',
  templateUrl: './masters-list.component.html',
  styleUrls: ['./masters-list.component.css']
})

export class MastersListComponent implements OnInit {

  @ViewChild("topMenu", {static: false})
  topMenu: ElementRef;

  @ViewChild("filter", {static: false})
  filter: ElementRef;

  public filterStatus: boolean = false;

  public vm: Vm = new Vm();

  constructor(private menu: MenuController, private masterList: MasterListClient.MasterListClient) {
  }

  ngOnInit(): void {
    this.masterList.getMasters(null, null, null, null, 0, 10).subscribe(data => {
      data.forEach(item => (item as any).starRating = this.countStarRating(item.averageRating));
      this.vm.Masters = data;
      console.log(data);
    });
  }

  public logScrollStart() {
    //console.log('scroll started');
  }

  public logScrolling($event: any) {
    //console.log($event.detail);
    if ($event.detail.deltaY > 10) {
      if ((this.topMenu as any).el.classList.contains('header-top')) {
        (this.topMenu as any).el.classList.remove('header-top');
      }
      //console.log('top');
    } else if ($event.detail.deltaY < -10) {
      (this.topMenu as any).el.classList.add('header-top');
      if ($event.detail.currentY === 0) {
        (this.topMenu as any).el.classList.remove('header-top');
      }
      //console.log('bot');
    }
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
  public Masters: any;
}
