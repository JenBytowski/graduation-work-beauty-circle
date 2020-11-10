import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenStoreService {
  private KEY: string = 'token';

  constructor() {
  }

  get(): string {
    if (!localStorage) {
      return null;
    }
    return localStorage.getItem(this.KEY);
  }

  put(token: string): void {
    localStorage.setItem(this.KEY, token);
  }

  clear(): void {
    localStorage.removeItem(this.KEY);
  }
}
