import { Injectable } from '@angular/core';
import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpResponse
} from "@angular/common/http";
import {Observable} from "rxjs";
import {tap} from "rxjs/operators";
import {TokenStoreService} from "./token-store.service";

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor{
  constructor(private tokenStore: TokenStoreService) {}
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const authReq = req.clone({
      headers: req.headers.set('Auth-Token', this.tokenStore.get())
    });
    return next.handle(authReq).pipe(
      tap(event => {
        if(event instanceof HttpResponse)
          console.log('Server response');
      }, err => {
        if(err instanceof HttpErrorResponse)
          console.log('Error: ', err.status);
      })
    );
  }
}
