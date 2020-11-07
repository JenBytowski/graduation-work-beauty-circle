import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {BookingClient} from "bc-common";

@Component({
  selector: 'app-day',
  templateUrl: './day.component.html',
  styleUrls: ['./day.component.scss']
})
export class DayComponent implements OnInit {
  public vm: Vm = new Vm();
  constructor(private route: ActivatedRoute, private booking: BookingClient.BookingClient) {
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    console.log(id);
    this.booking.getSchedule('D08AC59F-2155-48A5-84A9-59690294591B').subscribe(sch => {
      this.vm.scheduleDay = sch.days.find(item => item?.id === id.toString());
      console.log(this.vm.scheduleDay);
    });
  }

}
class Vm {
  public scheduleDay: any;
}
