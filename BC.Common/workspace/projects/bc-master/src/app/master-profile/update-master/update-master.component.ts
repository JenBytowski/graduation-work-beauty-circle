import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {JWTDecodeService, MasterListClient, TokenStoreService} from 'bc-common';

@Component({
  selector: 'app-update-master',
  templateUrl: './update-master.component.html',
  styleUrls: ['./update-master.component.scss'],
})
export class UpdateMasterComponent implements OnInit {
  @ViewChild('name', {static: false})
  name: ElementRef;
  @ViewChild('speciality', {static: false})
  speciality: ElementRef;
  @ViewChild('about', {static: false})
  about: ElementRef;
  @ViewChild('adderss', {static: false})
  adderss: ElementRef;
  @ViewChild('phone', {static: false})
  phone: ElementRef;
  @ViewChild('instagramProfile', {static: false})
  instagramProfile: ElementRef;
  @ViewChild('vkProfile', {static: false})
  vkProfile: ElementRef;
  @ViewChild('viber', {static: false})
  viber: ElementRef;
  @ViewChild('skype', {static: false})
  skype: ElementRef;

  public vm: Vm = new Vm();

  constructor(
    private route: ActivatedRoute,
    private masterList: MasterListClient.MasterListClient,
    private tokenStore: TokenStoreService,
    private jwtdecode: JWTDecodeService
  ) {
  }

  ngOnInit(): void {
    const id = this.jwtdecode.decode(this.tokenStore.get())['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
    this.masterList.getMasterById(id).subscribe((data) => {
      (data as any).starRating = this.countStarRating(data.averageRating);
      this.vm.Master = data;
      console.log(data);
    });
  }

  public countStarRating(rating: number) {
    rating = Math.round(rating * 2) / 2;
    return [
      rating >= 1
        ? 'star'
        : rating >= 0.5
        ? 'star-half-outline'
        : 'star-outline',
      rating >= 2
        ? 'star'
        : rating >= 1.5
        ? 'star-half-outline'
        : 'star-outline',
      rating >= 3
        ? 'star'
        : rating >= 2.5
        ? 'star-half-outline'
        : 'star-outline',
      rating >= 4
        ? 'star'
        : rating >= 3.5
        ? 'star-half-outline'
        : 'star-outline',
      rating >= 5
        ? 'star'
        : rating >= 4.5
        ? 'star-half-outline'
        : 'star-outline',
    ];
  }

  public updateMasterInfo(): void {
    let masterReq = MasterListClient.UpdateMasterReq.fromJS({
      name: ((this.name as any).el.value | this.vm.Master?.name).toString(),
      avatarUrl: this.vm.Master?.avatarUrl,
      about: ((this.about as any).el.value | this.vm.Master?.about).toString(),
      address: ((this.adderss as any).el.value | this.vm.Master?.address).toString(),
      phone: ((this.phone as any).el.value | this.vm.Master?.phone).toString(),
      instagramProfile:
        ((this.instagramProfile as any).el.value |
        this.vm.Master?.instagramProfile).toString(),
      vkProfile:
        ((this.vkProfile as any).el.value | this.vm.Master?.vkProfile).toString(),
      viber: ((this.viber as any).el.value | this.vm.Master?.viber).toString(),
      skype: ((this.skype as any).el.value | this.vm.Master?.skype).toString(),
      specialityId: this.vm.Master?.specialityId,
      priceListItems: this.vm.Master?.priceListItems,
    });
    console.log(masterReq);
    this.masterList
      .updateMaster(
        this.jwtdecode.decode(
          this.tokenStore.get())['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'], masterReq)
      .subscribe(() => window.location.reload());
  }
}

class Vm {
  public Master: any;
}
