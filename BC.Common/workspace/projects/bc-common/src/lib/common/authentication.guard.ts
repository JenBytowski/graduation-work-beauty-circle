import {
  ActivatedRouteSnapshot,
  CanActivate, Router,
  RouterStateSnapshot,
} from '@angular/router';
import { TokenStoreService } from './token-store.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TokenGuard implements CanActivate {
  constructor(private tokenStore: TokenStoreService, private router: Router) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): any{
    if(this.tokenStore.get()){
      return true;
    } else {
      this.router.navigate(['/authentication'],{
        queryParams: {
          return: state.url
        }});
      return false;
    }
  }
}
