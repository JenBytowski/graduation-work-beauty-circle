import {Injectable} from '@angular/core';
import * as jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class TokenStoreService {

  private readonly KEY = 'token';

  constructor() {
  }

  get(): string | null {
    if (!localStorage) {
      return null;
    }
    const token = localStorage.getItem(this.KEY);
    if (token === null || !this.isTokenExpired(token)) {
      this.clear();
      return null;
    }
    return token;
  }

  put(token: string): void {
    localStorage.setItem(this.KEY, token);
  }

  clear(): void {
    localStorage.removeItem(this.KEY);
  }

  isTokenExpired(token: string): boolean {
    const date = this.getTokenExpirationDate(token);
    if (date === null) {
      return false;
    }
    return date.valueOf() > new Date().valueOf();
  }

  getTokenExpirationDate(token: string): Date | null {
    const decoded: any = jwt_decode.default(token);
    if (decoded.exp === undefined) {
      return null;
    }
    const date = new Date(0);
    date.setUTCSeconds(decoded.exp);
    return date;
  }
}
