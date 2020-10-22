import { Component, OnInit } from '@angular/core';
import {MasterListClient, BookingClient, } from 'bc-common';

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
    this.booking.getSchedule('DEFF0772-EACB-4935-8623-7700C58B930A').subscribe(sch => {
      console.log(sch);
    });

    this.vm.currentIndex = 0;
    this.vm.schedule = [new ScheduleDay(), new ScheduleDay(), new ScheduleDay(), new ScheduleDay(), new ScheduleDay(), new ScheduleDay(), new ScheduleDay()];

    this.vm.schedule[0].day = new Date();
    this.vm.schedule[0].startTime = new Date();
    this.vm.schedule[0].endTime = new Date();
    this.vm.schedule[0].items = [new ScheduleDayItem()];
    this.vm.schedule[0].items[0].endTime = new Date();
    this.vm.schedule[0].items[0].startTime = new Date();

    this.vm.schedule[1].day = new Date(999999999);
    this.vm.schedule[1].startTime = new Date();
    this.vm.schedule[1].endTime = new Date();
    this.vm.schedule[1].items = [new ScheduleDayItem()];
    this.vm.schedule[1].items[0].endTime = new Date();
    this.vm.schedule[1].items[0].startTime = new Date();
  }
}

class Vm {
  public currentIndex: number;
  public schedule: ScheduleDay[];
}

class ScheduleDay {
  //public dayOfWeek: 'Monday' | 'Tuesday' | 'Wednesday' | 'Thursday' | 'Friday' | 'Saturday' | 'Sunday';
  public day: Date;
  public startTime: Date;
  public endTime: Date;
  public items: ScheduleDayItem[];
}

class ScheduleDayItem {
  public startTime: Date;
  public endTime: Date;
}
