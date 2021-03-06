import {Component, ElementRef, Inject, OnInit, ViewChild} from '@angular/core';
import {Subscription} from "rxjs";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthenticationClient, TokenStoreService} from "bc-common";
import {CookieService} from "ngx-cookie-service";

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.scss']
})
export class AuthenticationComponent implements OnInit {
  @ViewChild("code", {static: false})
  code: ElementRef;

  @ViewChild("phone", {static: false})
  phone: ElementRef;

  public codePopupStatus: boolean = false;
  public redirectUrl: string = this.baseUrl + 'authentication';
  private querySubscription: Subscription;

  constructor(
    private route: ActivatedRoute,
    private authClient: AuthenticationClient.AuthenticationClient,
    private tokenStore: TokenStoreService,
    private cookieService: CookieService, @Inject('BASE_URL')
    private baseUrl: string,
    private router: Router) {

  }

  async ngOnInit() {
    console.log(this.redirectUrl);//
    this.querySubscription = this.route.queryParams.subscribe((queryParam: any) => {
      const code = queryParam['code'];
      const state = queryParam['state'];
      queryParam['return'] ? localStorage.setItem('return', queryParam['return']) : {};
      if (code && state == 'vk') {
        this.authClient.authenticateByVk(AuthenticationClient.AuthenticationCodeRequest.fromJS({
          code: code,
          redirectUrl: this.redirectUrl,
          role: 'Master'
        })).subscribe(data => {
          if (data.token) {
            this.tokenStore.put(data.token);
            this.router.navigate([localStorage.getItem('return')]);
          }
        });
      } else if (code && state == 'instagram') {
        this.authClient.authenticateByInstagram(AuthenticationClient.AuthenticationCodeRequest.fromJS({
          code: code,
          redirectUrl: this.redirectUrl,
          role: 'Master'
        })).subscribe(data => {
          if (data.token) {
            this.tokenStore.put(data.token);
            this.router.navigate([localStorage.getItem('return')]);
          }
        });
      } else if (code && state == 'google') {
        this.authClient.authenticateByGoogle(AuthenticationClient.AuthenticationCodeRequest.fromJS({
          code: code,
          redirectUrl: this.redirectUrl,
          role: 'Master'
        })).subscribe(data => {
          if (data.token) {
            this.tokenStore.put(data.token);
            this.router.navigate([localStorage.getItem('return')]);
          }
        });
      }
    });
    if (this.tokenStore.get()) {
      await this.router.navigate([this.route.snapshot.queryParams.return || '/']);
    }
  }

  public async authByVk(): Promise<void> {
    window.location.href = `https://oauth.vk.com/authorize?client_id=7525094&redirect_uri=${this.redirectUrl}&display=page&response_type=code&scope=email&state=vk`;
  }

  public async authByInstagram(): Promise<void> {
    window.location.href = `https://api.instagram.com/oauth/authorize?client_id=1563532247147538&redirect_uri=${this.redirectUrl}&response_type=code&scope=user_profile,user_media&state=instagram`;
  }

  public async authByGoogle(): Promise<void> {
    window.location.href = `https://accounts.google.com/o/oauth2/v2/auth?client_id=4306410829-7gnaa751oumkn67o1jfbep4lpqlabnbe.apps.googleusercontent.com&response_type=code&redirect_uri=${this.redirectUrl}&state=google&scope=openid email`;
  }

  public async authByPhone() {
    await this.authClient.authenticateByPhoneStep1(AuthenticationClient.AuthenticationPhoneRequest.fromJS({
      phone: (this.phone as any).el.value,
      role: 'Master'
    })).toPromise();
  }

  public async sendCode() {
    if ((this.code as any).el.value) {
      await this.authClient.authenticateByPhoneStep2(AuthenticationClient.AuthenticatebyPhoneStep2Req.fromJS({
        phone: (this.phone as any).el.value,
        code: (this.code as any).el.value
      })).subscribe(data => {
        if (data.token) {
          this.tokenStore.put(data.token);
          this.router.navigate([localStorage.getItem('return')]);
        }
      });

    }
  }
}
