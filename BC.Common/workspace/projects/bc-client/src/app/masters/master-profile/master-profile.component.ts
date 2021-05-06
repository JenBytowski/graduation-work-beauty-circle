import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {IonRange} from '@ionic/angular';
import {MasterListClient, BookingClient, JWTDecodeService, TokenStoreService} from 'bc-common';

@Component({
  selector: 'app-master-profile',
  templateUrl: './master-profile.component.html',
  styleUrls: ['./master-profile.component.css'],
})
export class MasterProfileComponent implements OnInit {

  public vm: Vm = new Vm();
  public rangeMax: number;
  public rangeMin: number;

  constructor(
    private route: ActivatedRoute,
    private masterList: MasterListClient.MasterListClient,
    private booking: BookingClient.BookingClient,
    private tokenStore: TokenStoreService,
    private jwtdecode: JWTDecodeService
  ) {
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.booking.getSchedule(id).subscribe(data => {
      this.vm.Schedule = data;
      this.sortScheduleItems();
    });
    this.masterList.getMasterById(id).subscribe((data) => {
      (data as any).starRating = this.countStarRating(data.averageRating);
      this.vm.Master = data;
      this.vm.ClientId = this.jwtdecode.decode(this.tokenStore.get())[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
        ];
      console.log(this.vm);
    });
  }

  public redirect(url: string) {
    window.location.replace(url);
  }

  public getMastersSchedule() {
    this.booking.getSchedule(this.vm.Master.id).subscribe(data => console.log(data));
  }

  public sortScheduleItems() {
    (this.vm.Schedule as BookingClient.GetScheduleRes).days.forEach(day => {
      day.items.sort((item1, item2) => {
        return item1.startTime.getTime() - item2.startTime.getTime();
      })
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

  public changeItemState(event) {
    if (this.vm.CurrentItem.itemType == 'Window') {
      let range = (event.target.parentElement.parentElement.childNodes[2].childNodes[0] as IonRange).value;
      console.log(event.target.parentElement.parentElement);
      if ((range as any).lower < (range as any).upper) {
        let req: BookingClient.AddBookingReq = new BookingClient.AddBookingReq();
        req.masterId = this.vm.Master.id;
        req.clientId = this.vm.ClientId;
        req.serviceType = "C9F93769-75F9-4C85-8D5D-D6893D6887C5";
        req.description = 'Description';
        this.vm.CurrentItem.startTime.setHours((range as any).lower + 3);
        req.startTime = this.vm.CurrentItem.startTime;
        this.vm.CurrentItem.endTime.setHours((range as any).upper + 3);
        req.endTime = this.vm.CurrentItem.endTime;
        console.log(req);//
        console.log(this.vm.CurrentItem);//
        this.booking.addBooking(req).subscribe((data) => console.log(data));
        document.location.reload();
      }
    }
  }

  public enableRange(item: BookingClient.ScheduleDayItemRes) {
    if (item.itemType == "Window") {
      this.rangeMin = item.startTime.getHours();
      this.rangeMax = item.endTime.getHours();
      this.vm.CurrentItem = item;
    }

    if (item.itemType == 'Booking' && this.vm.ClientId == item.clientId) {
      let req: BookingClient.CancelBookingReq = new BookingClient.CancelBookingReq();
      req.bookingId = item.id;
      console.log(req);
      this.booking.cancelBooking(req).subscribe(data => console.log(data));
      document.location.reload();
    }
  }
}

class Vm {
  public Master: any;
  public Schedule: any;
  public ClientId: string;
  public CurrentItem: BookingClient.ScheduleDayItemRes;
}
