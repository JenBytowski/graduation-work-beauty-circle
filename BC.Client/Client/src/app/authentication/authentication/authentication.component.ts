import {Component, OnInit} from '@angular/core';
import {AuthenticationClient} from "../../api-client/authentication/clients";
import {Subscription} from "rxjs";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.scss'],
})
export class AuthenticationComponent implements OnInit {

  private code: string;

  private routeSubscription: Subscription;
  private querySubscription: Subscription;
  
  constructor(private route: ActivatedRoute ,private authClient: AuthenticationClient) {
  }

  ngOnInit() {
    try{
      this.querySubscription = this.route.queryParams.subscribe(
          (queryParam: any) => {this.code = queryParam['code'];});
      console.log(this.code);
    }
    catch (ex)
    {
      throw ex;
    }
  }
}
