import {Component, OnInit} from '@angular/core';
import {BookingClient, MasterListClient} from 'bc-common';

@Component({
  selector: 'app-week',
  templateUrl: './week.component.html',
  styleUrls: ['./week.component.scss']
})
export class WeekComponent implements OnInit {

  public vm: Vm = new Vm();

  constructor(private masterList: MasterListClient.MasterListClient, private booking: BookingClient.BookingClient) {
  }

  ngOnInit() {
    //this.masterList.getMasters(null,null,null,null).subscribe(data => console.log(data));
    this.booking.getSchedule('D08AC59F-2155-48A5-84A9-59690294591B').subscribe(sch => {
      this.vm.schedule = sch;
      console.log(sch);
    });
  }
}

class Vm {
  public schedule: any;
}
