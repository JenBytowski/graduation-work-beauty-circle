import {Component, OnInit} from '@angular/core';
import {BookingClient, JWTDecodeService, MasterListClient, TokenStoreService} from 'bc-common';
import {Router} from "@angular/router";

@Component({
  selector: 'app-week',
  templateUrl: './week.component.html',
  styleUrls: ['./week.component.scss']
})
export class WeekComponent implements OnInit {

  public vm: Vm = new Vm();

  constructor(
    private masterList: MasterListClient.MasterListClient,
    private booking: BookingClient.BookingClient,
    private jwtdecode: JWTDecodeService,
    public tokenStore: TokenStoreService,
    private router: Router
    ) {
  }

  ngOnInit() {
    this.booking.getSchedule(this.jwtdecode.decode(this.tokenStore.get())['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']).subscribe(sch => {
      this.vm.schedule = sch;
      console.log(sch);
    });
  }

  async logOut(){
    this.tokenStore.get() ? this.tokenStore.clear() : {};
    await this.router.navigate(['/authentication']);
  }
}

class Vm {
  public schedule: any;
}
