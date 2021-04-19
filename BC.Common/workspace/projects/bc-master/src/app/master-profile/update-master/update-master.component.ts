import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {JWTDecodeService, MasterListClient, TokenStoreService, BookingClient} from 'bc-common';

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
  @ViewChild('address', {static: false})
  address: ElementRef;
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
    private booking: BookingClient.BookingClient,
    public tokenStore: TokenStoreService,
    private jwtdecode: JWTDecodeService,
    private router: Router
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
      name: (this.name as any).el.value ? (this.name as any).el.value.toString() : undefined,
      avatarUrl: this.vm.Master?.avatarUrl,
      about: (this.about as any).el.value ? (this.about as any).el.value.toString() : undefined,
      address: (this.address as any).el.value ? (this.address as any).el.value.toString() : undefined,
      phone: (this.phone as any).el.value ? (this.phone as any).el.value.toString() : undefined,
      instagramProfile: (this.instagramProfile as any).el.value ? (this.instagramProfile as any).el.value.toString() : undefined,
      vkProfile: (this.vkProfile as any).el.value ? (this.vkProfile as any).el.value.toString() : undefined,
      viber: (this.viber as any).el.value ? (this.viber as any).el.value.toString() : undefined,
      skype: (this.skype as any).el.value ? (this.skype as any).el.value.toString() : undefined,
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

  public async addWeek() {
    this.booking.getSchedule(this.vm.Master.id).subscribe(sch => {
      this.vm.Schedule = sch;
      if (this.getNextMonDate().valueOf() > this.getLastDay(sch.days)) {
        console.log('Days added!')
        this.booking.addWorkingWeek(BookingClient.AddWorkingWeekReq.fromJS({
          masterId: this.vm.Master.id,
          mondayDate: this.getNextMonDate(),
          daysToWork: [1, 2, 3, 4, 5],
          startTime: "8:00",
          endTime: "23:00"
        })).subscribe(data => console.log(data));
      }
    });
  }

  public getNextMonDate(): Date {
    let d = new Date();
    d.setDate(d.getDate() + (1 + 7 - d.getDay()) % 7);
    return d;
  }

  public getLastDay(days: BookingClient.ScheduleDayRes[]): number {
    let last: number | object = new Date().valueOf();
    days.forEach(day => {
      if(last < day.date.valueOf()){
        last = day.date.valueOf();
      }
    });
    return last;
  }

  async logOut(){
    this.tokenStore.get() ? this.tokenStore.clear() : {};
    await this.router.navigate(['/authentication']);
  }
}

class Vm {
  public Master: any;
  public Schedule: BookingClient.GetScheduleRes;
}
