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
  public starRating: string[];
}

class PriceListItem {

}

class ScheduleDay {

}