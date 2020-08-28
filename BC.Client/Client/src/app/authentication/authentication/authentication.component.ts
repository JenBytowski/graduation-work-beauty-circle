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

  private querySubscription: Subscription;

  constructor(private route: ActivatedRoute, private authClient: AuthenticationClient) {
  }

  ngOnInit() {
    this.querySubscription = this.route.queryParams.subscribe((queryParam: any) => {
      const code = queryParam['code'];
      console.log(code);
      if (code) {
        this.authClient.authenticateByVk(code).subscribe(data => console.log(data));
      }
    });
  }

  public async authByPhone() {
    await this.authClient.getSmsAuthenticationCode('375299854478').toPromise();
  }
}
