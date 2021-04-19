import {Injectable} from '@angular/core';
import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpResponse,
} from '@angular/common/http';
import {Observable} from 'rxjs';
import {tap} from 'rxjs/operators';
import {TokenStoreService} from './token-store.service';

@Injectable({
  providedIn: 'root',
})
export class AuthInterceptorService implements HttpInterceptor {
  constructor(private tokenStore: TokenStoreService) {
  }

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (this.tokenStore.get()) {
      const authReq = req.clone({
        headers: req.headers.set(
          'Authorization',
          'Bearer ' + this.tokenStore.get()
        ),
      });
      return next.handle(authReq).pipe(
        tap(
          (event) => {
            if (event instanceof HttpResponse) console.log(event);
          },
          (err) => {
            if (err instanceof HttpErrorResponse) console.log(err);
          }
        )
      );
    } else {
      return next.handle(req).pipe(
        tap(
          (event) => {
            if (event instanceof HttpResponse) console.log(event);
          },
          (err) => {
            if (err instanceof HttpErrorResponse) console.log(err);
          }
        )
      );
    }
  }
}
