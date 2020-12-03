import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {BookingClient, JWTDecodeService, TokenStoreService} from "bc-common";

@Component({
  selector: 'app-day',
  templateUrl: './day.component.html',
  styleUrls: ['./day.component.scss']
})
export class DayComponent implements OnInit {
  public vm: Vm = new Vm();
  constructor(
    private route: ActivatedRoute,
    private booking: BookingClient.BookingClient,
    private tokenStore: TokenStoreService,
    private jwtdecode: JWTDecodeService) {
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    console.log(id);
    this.booking.getSchedule(this.jwtdecode.decode(this.tokenStore.get()['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'])).subscribe(sch => {
      this.vm.scheduleDay = sch.days.find(item => item?.id === id.toString());
      console.log(this.vm.scheduleDay);
    });
  }

}
class Vm {
  public scheduleDay: any;
}
