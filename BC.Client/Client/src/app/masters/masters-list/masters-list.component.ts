import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {MenuController} from '@ionic/angular';
import {MasterListClient} from "../../api-client/master-list/clients";

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

  constructor(private menu: MenuController, private masterList: MasterListClient) {
  }

  ngOnInit(): void {
    this.masterList.getMasters(null, null, null, null).subscribe(data => {
      this.vm = this.initMasters(data);
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

  public initMasters(masters: any): Vm {
    let vm = new Vm();
    let index: number = 0;
    vm.Masters = masters.map(item => {
      let master = new Master();
      master.id = index;
      master.name = item.name;
      master.cityId = item.cityId;
      master.speciality = item.speciality;
      master.avatarUrl = item.avatarUrl ? item.avatarUrl : 'https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcRgAefR8jkzapHvRFbIIug_q3PcaqYmmbWdDQ&usqp=CAU';
      master.about = item.about;
      master.address = item.address;
      master.phone = item.phone;
      master.instagramProfile = item.instagramProfile;
      master.vkProfile = item.vkProfile;
      master.viber = item.viber;
      master.skype = item.skype;
      master.priceList = item.priceList;
      master.schedule = item.schedule;
      master.averageRating = item.averageRating;
      master.starRating = this.countStarRating(item.averageRating);
      index++;
      return master;
    });
    return vm;
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
  public Masters: Master[] = [];
}

class Master {
  public id: number;
  public name: string;
  public cityId: string;
  public avatarUrl: string;
  public about: string;
  public address: string;
  public phone: string;
  public instagramProfile: string;
  public vkProfile: string;
  public viber: string;
  public skype: string;
  public speciality: string;
  public priceList: PriceListItem[];
  public schedule: ScheduleDay[];
  public averageRating: number;
  public starRating: string[];
}

class PriceListItem {

}

class ScheduleDay {

}