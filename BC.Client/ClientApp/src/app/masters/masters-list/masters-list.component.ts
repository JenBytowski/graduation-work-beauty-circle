import { Component, OnInit, ViewChild, ElementRef} from '@angular/core';
import {MenuController} from '@ionic/angular';

@Component({
  selector: 'app-masters-list',
  templateUrl: './masters-list.component.html',
  styleUrls: ['./masters-list.component.css']
})

export class MastersListComponent implements OnInit {
  @ViewChild("topMenu", {static: false})
  topMenu: ElementRef;
  public vm: Vm = this.initMasters();
  constructor(private menu: MenuController) { }
  ngOnInit(): void {
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
      if ((this.topMenu as any).el.classList.contains('test_header_top')) {
        (this.topMenu as any).el.classList.remove('test_header_top');
      }
      //console.log('top');
    } else if ($event.detail.deltaY < -10) {
      (this.topMenu as any).el.classList.add('test_header_top');
      if ($event.detail.currentY === 0) {
        (this.topMenu as any).el.classList.remove('test_header_top');
      }
      //console.log('bot');
    }
  }
  initMasters(): Vm{
    let vm = new Vm();
    for(let i = 0; i < 20; i++){
      vm.Masters[i] = new Master();
      vm.Masters[i].Name = i.toString();
      vm.Masters[i].Service = i.toString();
      vm.Masters[i].Avatar = 'https://24smi.org/public/media/celebrity/2019/04/16/ebullttytnug-sergei-zverev.jpg';
    }
    return vm;
  }
}

class Vm{
  public Masters: Master[] = [];
}

class Master{
  public Name: string;
  public Service: string;
  public Avatar: string;
}
