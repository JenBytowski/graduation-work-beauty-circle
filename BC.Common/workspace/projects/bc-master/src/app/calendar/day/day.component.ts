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
    const id = +this.route.snapshot.paramMap.get('id');
    console.log(id);
    this.booking.getSchedule('deff0772-eacb-4935-8623-7700c58b930a').subscribe(sch => {
      this.vm.scheduleDay = sch.days.find(item => item?.id === id.toString());
      console.log(this.vm.scheduleDay);
    });
  }

}
class Vm {
  public scheduleDay: any;
}
