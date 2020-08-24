import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {MastrerListService} from "../../../../backend/services";

@Component({
  selector: 'app-master-profile',
  templateUrl: './master-profile.component.html',
  styleUrls: ['./master-profile.component.css']
})
export class MasterProfileComponent implements OnInit {

  public vm: Vm = new Vm();

  constructor(private route: ActivatedRoute, private service: MastrerListService) { }

  ngOnInit(): void {
    this.vm.Master = new Master();
    const id = +this.route.snapshot.paramMap.get('id');
    this.service.getMastersList().subscribe(data => this.vm.Master = this.initMasters(data).Masters.find(item => item.Id == id));
  }

  initMasters(masters: any): Vm{
    let vm = new Vm();
    for(let i = 0; i < masters.length; i++){
      vm.Masters[i] = new Master();
      vm.Masters[i].Id = i;
      vm.Masters[i].Name = masters[i].name;
      vm.Masters[i].Spec = i.toString();
      vm.Masters[i].Avatar = 'https://24smi.org/public/media/celebrity/2019/04/16/ebullttytnug-sergei-zverev.jpg';
    }
    return vm;
  }
}

class Vm{
  public Masters: Master[] = [];
  public Master: Master;
}

class Master{
  public Id: number;
  public Name: string;
  public Spec: string;
  public Avatar: string;
}
