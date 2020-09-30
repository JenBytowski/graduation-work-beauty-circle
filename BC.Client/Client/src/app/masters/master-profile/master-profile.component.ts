import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {MasterListClient} from "../../api-client/master-list/clients";

@Component({
  selector: 'app-master-profile',
  templateUrl: './master-profile.component.html',
  styleUrls: ['./master-profile.component.css']
})
export class MasterProfileComponent implements OnInit {

  public vm: Vm = new Vm();

  constructor(private route: ActivatedRoute, private masterList: MasterListClient) {
  }

  ngOnInit(): void {
    this.vm.Master = new Master();
    const id = +this.route.snapshot.paramMap.get('id');
    this.masterList.getMasters(null, null, null, null).subscribe(data => this.vm.Master = this.initMasters(data).Masters.find(item => item.id == id));
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
  public Master: Master;
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