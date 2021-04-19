import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
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
    console.log(id);
  }

  public cancelBooking(id: string) {
    console.log(id);
  }

  async logOut(){
    this.tokenStore.get() ? this.tokenStore.clear() : {};
    await this.router.navigate(['/authentication']);
  }

}

class Vm {
  public day: BookingClient.ScheduleDayRes;
}
