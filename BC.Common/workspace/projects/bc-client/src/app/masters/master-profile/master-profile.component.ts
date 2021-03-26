import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MasterListClient, BookingClient, JWTDecodeService, TokenStoreService } from 'bc-common';

@Component({
  selector: 'app-master-profile',
  templateUrl: './master-profile.component.html',
  styleUrls: ['./master-profile.component.css'],
})
export class MasterProfileComponent implements OnInit {

  public vm: Vm = new Vm();

  @ViewChild("itemDate", { static: false })
  itemDate: ElementRef;
  @ViewChild("itemTime", { static: false })
  itemTime: ElementRef;

  constructor(
    private route: ActivatedRoute,
    private masterList: MasterListClient.MasterListClient,
    private booking: BookingClient.BookingClient,
    private tokenStore: TokenStoreService,
    private jwtdecode: JWTDecodeService
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.booking.getSchedule(id).subscribe(data => this.vm.Schedule = data);
    this.masterList.getMasterById(id).subscribe((data) => {
      (data as any).starRating = this.countStarRating(data.averageRating);
      this.vm.Master = data;
      this.vm.ClientId = this.jwtdecode.decode(this.tokenStore.get())[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
      ];
      console.log(this.vm);
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

  public redirect(url: string) {
    window.location.replace(url);
  }

  public getMastersSchedule() {
    this.booking.getSchedule(this.vm.Master.id).subscribe(data => console.log(data));
  }

  public changeItemState(item: BookingClient.ScheduleDayItemRes) {
    if (item.itemType == 'Window') {
      let req: BookingClient.AddBookingReq = new BookingClient.AddBookingReq();
      req.masterId = this.vm.Master.id;
      req.clientId = this.vm.ClientId;
      req.serviceType = "C9F93769-75F9-4C85-8D5D-D6893D6887C5";
      req.description = 'Description';
      item.startTime.setHours(item.startTime.getHours() + 3);
      req.startTime = item.startTime;
      item.endTime.setHours(item.endTime.getHours() + 3);
      req.endTime = item.endTime;
      console.log(req);//
      console.log(item);//
      //this.booking.addBooking(req).subscribe((data) => console.log(data));
      //document.location.reload();
    }
    else if (item.itemType == 'Booking' && this.vm.ClientId == item.clientId) {
      let req: BookingClient.CancelBookingReq = new BookingClient.CancelBookingReq();
      req.bookingId = item.id;
      console.log(req);
      //this.booking.cancelBooking(req).subscribe(data => console.log(data));
      //document.location.reload();
    }
  }

  public ShowItems(): void {
    (this.itemDate as any).el.style.display = "none";
    (this.itemTime as any).el.style.display = "inline";
  }
}

class Vm {
  public Master: any;
  public Schedule: BookingClient.GetScheduleRes;
  public ClientId: string;
}
