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
import {CookieService} from "ngx-cookie-service";

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor{
  constructor() {}
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const authReq = req.clone({
      headers: req.headers.set('Vk-Auth-Token', '123123123')
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
