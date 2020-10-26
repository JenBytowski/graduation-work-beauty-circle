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
    this.booking.getSchedule('deff0772-eacb-4935-8623-7700c58b930a').subscribe(sch => {
      this.vm.schedule = sch;
      console.log(sch);
    });
  }
}

class Vm {
  public currentIndex: number;
  public schedule: any;
}
