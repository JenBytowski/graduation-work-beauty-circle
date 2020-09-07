import {Component, ElementRef, Inject, OnInit, ViewChild} from '@angular/core';
import {
  AuthenticationClient,
  AuthenticationCodeRequest,
  AuthenticationPhoneRequest,
  SMSCodeAuthenticationResponse
} from "../../api-client/authentication/clients";
import {Subscription} from "rxjs";
import {ActivatedRoute} from "@angular/router";
import {CookieService} from "ngx-cookie-service";

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.scss'],
})

export class AuthenticationComponent implements OnInit {

  @ViewChild("code", {static: false})
  code: ElementRef;
  private querySubscription: Subscription;
  public redirectUrl: string = this.baseUrl + 'authentication';

  constructor(private route: ActivatedRoute, private authClient: AuthenticationClient, private cookieService: CookieService, @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit() {
    console.log(this.redirectUrl)
    this.querySubscription = this.route.queryParams.subscribe((queryParam: any) => {
      const code = queryParam['code'];
      const state = queryParam['state'];
      if (code && state == 'vk') {
        this.authClient.authenticateByVk(AuthenticationCodeRequest.fromJS({code: code})).subscribe(data => {
          this.cookieService.set('vk-auth-token', data.token);
          this.cookieService.set('vk-username', data.username);
        });
      } else if (code && state == 'instagram') {
        this.authClient.authenticateByInstagram(AuthenticationCodeRequest.fromJS({code: code})).subscribe(data => {
          this.cookieService.set('inst-auth-token', data.token);
          this.cookieService.set('inst-username', data.username);
        });
      } else if (code && state == 'google') {
        this.authClient.authenticateByGoogle(AuthenticationCodeRequest.fromJS({code: code})).subscribe(data => {
          this.cookieService.set('google-auth-token', data.token);
          this.cookieService.set('google-username', data.username);
        });
      }
    });
  }

  public async authByPhone() {
    await this.authClient.getSmsAuthenticationCode(AuthenticationPhoneRequest.fromJS({phone: '375299854478'})).toPromise();
  }

  public async sendCode() {
    if ((this.code as any).el.value) {
      await this.authClient.authenticateByPhone(SMSCodeAuthenticationResponse.fromJS({
        phone: '375299854478',
        code: (this.code as any).el.value
      })).subscribe(data => console.log(data));

    }
  }
}
