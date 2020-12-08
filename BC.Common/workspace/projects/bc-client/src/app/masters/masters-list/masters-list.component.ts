import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {MenuController} from '@ionic/angular';
import {MasterListClient, TokenStoreService} from "bc-common";

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

  @ViewChild('cityId', {static: false})
  cityId: ElementRef;
  @ViewChild('serviceTypes', {static: false})
  serviceTypes: ElementRef;
  @ViewChild('startHour', {static: false})
  startHour: ElementRef;
  @ViewChild('endHour', {static: false})
  endHour: ElementRef;
  @ViewChild('skip', {static: false})
  skip: ElementRef;
  @ViewChild('take', {static: false})
  take: ElementRef;

  public filterStatus: boolean = false;
  public vm: Vm = new Vm();
  public isAuthorized: boolean;

  constructor(
    private menu: MenuController,
    private masterList: MasterListClient.MasterListClient,
    private tokenStore: TokenStoreService) {
  }

  ngOnInit(): void {
    this.vm.filtre = new Filtre();
    this.fillFiltre();
    console.log(this.vm);
    this.tokenStore.get() ? this.isAuthorized = true : this.isAuthorized = false;
    console.log(this.isAuthorized);//
    this.improveGetMasters(this.vm.filtre);
  }

  public regetMasters(){
    this.fillFiltre();
    console.log(this.vm);
    this.improveGetMasters(this.vm.filtre);
  }

  public improveGetMasters(filtre: Filtre){
    this.masterList.getMasters(
      filtre.cityId,
      filtre.serviceTypeIds,
      filtre.startHour,
      filtre.endHour,
      filtre.skip,
      filtre.take).subscribe(data => {
      data.forEach(item => (item as any).starRating = this.countStarRating(item.averageRating));
      this.vm.Masters = data;
      console.log(data);//
    });
  }

  public fillFiltre(){
    this.vm.filtre.cityId = (this.cityId as any)?.el.value ? (this.cityId as any).el.value.toString() : null;
    this.vm.filtre.serviceTypeIds = (this.serviceTypes as any)?.el.value ? (this.serviceTypes as any).el.value.toString().split(' ') : null;
    this.vm.filtre.startHour = (this.startHour as any)?.el.value ? (this.startHour as any).el.value.toString() : null;
    this.vm.filtre.endHour = (this.endHour as any)?.el.value ? (this.endHour as any).el.value.toString() : null;
    this.vm.filtre.skip = (this.skip as any)?.el.value ? (this.skip as any).el.value.toString() : 0;
    this.vm.filtre.take = (this.take as any)?.el.value ? (this.take as any).el.value.toString() : 10;
  }

  public logScrollStart() {
    //console.log('scroll started');//
  }

  public logScrolling($event: any) {
    //console.log($event.detail);//
    if ($event.detail.deltaY > 10) {
      if ((this.topMenu as any).el.classList.contains('header-top')) {
        (this.topMenu as any).el.classList.remove('header-top');
      }
      //console.log('top');//
    } else if ($event.detail.deltaY < -10) {
      (this.topMenu as any).el.classList.add('header-top');
      if ($event.detail.currentY === 0) {
        (this.topMenu as any).el.classList.remove('header-top');
      }
      //console.log('bot');//
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
  public filtre: Filtre;
}

class Filtre {
  public cityId: string | null | undefined;
  public serviceTypeIds: string[] | null | undefined;
  public startHour: number | null | undefined;
  public endHour: number | null | undefined;
  public skip: number | undefined;
  public take: number | undefined;
}
