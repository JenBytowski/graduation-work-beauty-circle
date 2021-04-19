import {Component, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {BookingClient, JWTDecodeService, TokenStoreService} from "bc-common";
import {IonRange} from "@ionic/angular";

@Component({
  selector: 'app-day',
  templateUrl: './day.component.html',
  styleUrls: ['./day.component.scss']
})
export class DayComponent implements OnInit {

  public vm: Vm = new Vm();
  public rangeMax: number;
  public rangeMin: number;

  @ViewChild("range", {static: false})
  range: IonRange;

  constructor(
    private route: ActivatedRoute,
    private booking: BookingClient.BookingClient,
    public tokenStore: TokenStoreService,
    private jwtdecode: JWTDecodeService,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.booking.getSchedule(this.jwtdecode.decode(this.tokenStore.get())['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']).subscribe(sch => {
      this.vm.day = sch.days.find(item => item?.id === id.toString());
      console.log(this.vm.day);
    });
  }

  public cancelPause(id: string) {
    this.booking.cancelPause(BookingClient.CancelPauseReq.fromJS({
      pauseId: id
    })).subscribe(data => console.log(data));
    document.location.reload();
  }

  public cancelBooking(id: string) {
    this.booking.cancelBooking(BookingClient.CancelBookingReq.fromJS({
      bookingId: id
    })).subscribe(data => console.log(data));
    document.location.reload();
  }

  public addPause(event) {
    let range = (event.target.parentElement.childNodes[0] as IonRange).value;
    if ((range as any).lower < (range as any).upper) {
      let req: BookingClient.AddPauseReq = new BookingClient.AddPauseReq();
      req.masterId = this.jwtdecode.decode(this.tokenStore.get())['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
      req.description = 'Description';
      this.vm.CurrentItem.startTime.setHours((range as any).lower + 3);
      req.startTime = this.vm.CurrentItem.startTime;
      this.vm.CurrentItem.endTime.setHours((range as any).upper + 3);
      req.endTime = this.vm.CurrentItem.endTime;
      console.log(req);//
      console.log(this.vm.CurrentItem);//
      this.booking.addPause(req).subscribe(data => console.log(data));
      document.location.reload();
    }
  }

  public enableRange(item: BookingClient.ScheduleDayItemRes) {
    if (item.itemType == "Window") {
      (this.range as any).el.disabled = false;
      this.rangeMin = item.startTime.getHours();
      this.rangeMax = item.endTime.getHours();
      this.vm.CurrentItem = item;
    }
  }

  async logOut() {
    this.tokenStore.get() ? this.tokenStore.clear() : {};
    await this.router.navigate(['/authentication']);
  }

}

class Vm {
  public day: BookingClient.ScheduleDayRes;
  public CurrentItem: BookingClient.ScheduleDayItemRes;
}
