import {Component, OnInit} from '@angular/core';
import {BookingClient, JWTDecodeService, MasterListClient, TokenStoreService} from 'bc-common';

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
    private tokenStore: TokenStoreService) {
  }

  ngOnInit() {
    this.booking.getSchedule(this.jwtdecode.decode(this.tokenStore.get())['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']).subscribe(sch => {
      this.vm.schedule = sch;
      console.log(sch);
    });
  }
}

class Vm {
  public schedule: any;
}
