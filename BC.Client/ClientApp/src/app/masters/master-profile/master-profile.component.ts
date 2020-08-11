import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-master-profile',
  templateUrl: './master-profile.component.html',
  styleUrls: ['./master-profile.component.css']
})
export class MasterProfileComponent implements OnInit {

  public vm: Vm = new Vm();

  constructor(private route: ActivatedRoute,) { }

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.vm.Master = this.initMasters().Masters.find(item => item.Id == id);
  }

  initMasters(): Vm{
    let vm = new Vm();
    for(let i = 0; i < 20; i++){
      vm.Masters[i] = new Master();
      vm.Masters[i].Id = i;
      vm.Masters[i].Name = i.toString();
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
