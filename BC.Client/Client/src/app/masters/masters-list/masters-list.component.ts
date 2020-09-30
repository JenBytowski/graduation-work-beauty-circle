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
    this.masterList.getMasters(null, null, null, null).subscribe(data => this.vm = this.initMasters(data));
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
      master.avatarUrl = 'https://24smi.org/public/media/celebrity/2019/04/16/ebullttytnug-sergei-zverev.jpg';
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
      index++;
      return master;
    });
    return vm;
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
}

class PriceListItem {

}

class ScheduleDay {

}