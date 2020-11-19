import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from "@angular/router";
import {Observable} from "rxjs";
import {TokenStoreService} from "../common/token-store.service";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class TokenGuard implements CanActivate {
  constructor(private tokenStore: TokenStoreService, private router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
    if (this.tokenStore.get()) {
      return true;
    } else {
      this.router.navigateByUrl('/authentication?return-url=' + location.href).then(() => {
        return false
      });
      //this.router.navigateByUrl('/unauthorized').then(() => {return false});
    }
  }
}
