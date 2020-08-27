import {Component, OnInit, ViewChild, ElementRef} from '@angular/core';
import {MenuController} from '@ionic/angular';
import {MasterListClient} from "../../api-client/master-list/clients";

@Component({
  selector: 'app-masters-list',
  templateUrl: './masters-list.component.html',
  styleUrls: ['./masters-list.component.css']
})

export class MastersListComponent implements OnInit {

  @ViewChild("topMenu", {static: false})
  topMenu: ElementRef;

  @ViewChild("filter", {static: false})
  filter: ElementRef;

  public filterStatus: boolean = false;

  public vm: Vm = new Vm();

  constructor(private menu: MenuController, private masterList: MasterListClient) {
  }

  ngOnInit(): void {
    this.masterList.mastersList().subscribe(data => this.vm = this.initMasters(data));
  }

  openFirst() {
    this.menu.enable(true, 'first');
    this.menu.open('first');
  }

  public logScrollStart() {
    //console.log('scroll started');
  }

  public logScrolling($event: any) {
    //console.log($event.detail);
    if ($event.detail.deltaY > 10) {
      if ((this.topMenu as any).el.classList.contains('header-top')) {
        (this.topMenu as any).el.classList.remove('header-top');
      }
      //console.log('top');
    } else if ($event.detail.deltaY < -10) {
      (this.topMenu as any).el.classList.add('header-top');
      if ($event.detail.currentY === 0) {
        (this.topMenu as any).el.classList.remove('header-top');
      }
      //console.log('bot');
    }
  }

  public initMasters(masters: any): Vm {
    let vm = new Vm();
    vm.Masters = masters.map(item => {
      let master = new Master();
      master.Id = item.index;
      master.Name = item.name;
      master.Spec = item.index;
      master.Avatar = 'https://24smi.org/public/media/celebrity/2019/04/16/ebullttytnug-sergei-zverev.jpg';
      return master;
    });
    return vm;
  }
}

class Vm {
  public Masters: Master[] = [];
}

class Master {
  public Id: number;
  public Name: string;
  public Spec: string;
  public Avatar: string;
}
