import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {AuthenticationClient, SMSCodeAuthenticationResponse} from "../../api-client/authentication/clients";
import {Subscription} from "rxjs";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.scss'],
})
export class AuthenticationComponent implements OnInit {

  @ViewChild("code", {static: false})
  code: ElementRef;

  private querySubscription: Subscription;

  constructor(private route: ActivatedRoute, private authClient: AuthenticationClient) {
  }

  ngOnInit() {
    this.querySubscription = this.route.queryParams.subscribe((queryParam: any) => {
      const code = queryParam['code'];
      const state = queryParam['state'];
      console.log(code);
      if (code && state == 'vk') {
        this.authClient.authenticateByVk(code).subscribe(data => console.log(data));
      } else if(code && state == 'instagram'){
        this.authClient.authenticateByInstagram(code).subscribe(data => console.log(data));
      } else if(code && state == 'google'){
        this.authClient.authenticateByGoogle(code).subscribe(data => console.log(data));
      }
    });
  }

  public async authByPhone() {
    await this.authClient.getSmsAuthenticationCode('375299854478').toPromise();
  }
  public async sendCode() {
    if((this.code as any).el.value){
      await this.authClient.authenticateByPhone(SMSCodeAuthenticationResponse.fromJS({phone: '375299854478',code: (this.code as any).el.value})).subscribe(data => console.log(data));

    }
  }
}
