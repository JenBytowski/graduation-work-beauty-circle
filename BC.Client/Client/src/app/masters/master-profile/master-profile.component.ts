import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {MasterListClient} from "../../api-client/master-list/clients";

@Component({
  selector: 'app-master-profile',
  templateUrl: './master-profile.component.html',
  styleUrls: ['./master-profile.component.css']
})
export class MasterProfileComponent implements OnInit {

  public vm: Vm = new Vm();

  constructor(private route: ActivatedRoute, private masterList: MasterListClient) {
  }

  ngOnInit(): void {
    this.vm.Master = new Master();
    const id = +this.route.snapshot.paramMap.get('id');
    this.masterList.mastersList().subscribe(data => this.vm.Master = this.initMasters(data).Masters.find(item => item.Id == id));
  }

  public initMasters(masters: any): Vm {
    let vm = new Vm();
    let index: number = 0;
    vm.Masters = masters.map(item => {
      let master = new Master();
      master.Id = index;
      master.Name = item.name;
      master.Spec = index.toString();
      master.Avatar = 'https://24smi.org/public/media/celebrity/2019/04/16/ebullttytnug-sergei-zverev.jpg';
      index++;
      return master;
    });
    return vm;
  }
}

class Vm {
  public Masters: Master[] = [];
  public Master: Master;
}

class Master {
  public Id: number;
  public Name: string;
  public Spec: string;
  public Avatar: string;
}
