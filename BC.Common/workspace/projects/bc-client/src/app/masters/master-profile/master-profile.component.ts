import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MasterListClient, BookingClient, JWTDecodeService, TokenStoreService} from 'bc-common';

@Component({
  selector: 'app-master-profile',
  templateUrl: './master-profile.component.html',
  styleUrls: ['./master-profile.component.css'],
})
export class MasterProfileComponent implements OnInit {
  public vm: Vm = new Vm();

  constructor(
    private route: ActivatedRoute,
    private masterList: MasterListClient.MasterListClient,
    private booking: BookingClient.BookingClient,
    private tokenStore: TokenStoreService,
    private jwtdecode: JWTDecodeService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.masterList.getMasterById(id).subscribe((data) => {
      (data as any).starRating = this.countStarRating(data.averageRating);
      this.vm.Master = data;
      this.vm.ClientId = this.jwtdecode.decode(this.tokenStore.get())[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
      ];
      console.log(data);
    });
  }

  public countStarRating(rating: number) {
    rating = Math.round(rating * 2) / 2;
    return [
      rating >= 1
        ? 'star'
        : rating >= 0.5
        ? 'star-half-outline'
        : 'star-outline',
      rating >= 2
        ? 'star'
        : rating >= 1.5
        ? 'star-half-outline'
        : 'star-outline',
      rating >= 3
        ? 'star'
        : rating >= 2.5
        ? 'star-half-outline'
        : 'star-outline',
      rating >= 4
        ? 'star'
        : rating >= 3.5
        ? 'star-half-outline'
        : 'star-outline',
      rating >= 5
        ? 'star'
        : rating >= 4.5
        ? 'star-half-outline'
        : 'star-outline',
    ];
  }

  public addBooking() {
    let req: BookingClient.AddBookingReq = new BookingClient.AddBookingReq();
    req.masterId = this.vm.Master.id;
    req.clientId = this.vm.ClientId;
    req.serviceType = null;
    req.description = 'Description';
    req.startTime = new Date(0);
    req.endTime = new Date(10000);
    console.log(req);//
    this.booking.addBooking(req).subscribe((data) => console.log(data));
  }
}

class Vm {
  public Master: any;
  public ClientId: string;
}
