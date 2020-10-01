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
            const role = queryParam['role'];
            console.log(role);
            if (code && state == 'vk') {
                this.authClient.authenticateByVk(AuthenticationCodeRequest.fromJS({
                    code: code,
                    redirectUrl: this.redirectUrl
                })).subscribe(data => {
                    this.cookieService.set('vk-auth-token', data.token);
                    this.cookieService.set('vk-username', data.username);
                });
            } else if (code && state == 'instagram') {
                this.authClient.authenticateByInstagram(AuthenticationCodeRequest.fromJS({
                    code: code,
                    redirectUrl: this.redirectUrl
                })).subscribe(data => {
                    this.cookieService.set('inst-auth-token', data.token);
                    this.cookieService.set('inst-username', data.username);
                });
            } else if (code && state == 'google') {
                this.authClient.authenticateByGoogle(AuthenticationCodeRequest.fromJS({
                    code: code,
                    redirectUrl: this.redirectUrl
                })).subscribe(data => {
                    this.cookieService.set('google-auth-token', data.token);
                    this.cookieService.set('google-username', data.username);
                });
            }
        });
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