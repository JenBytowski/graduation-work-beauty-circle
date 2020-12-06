import { Injectable } from '@angular/core';
import * as jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class JWTDecodeService {
  constructor() {}
  decode(token: string): any {
    return jwt_decode.default(token);
  }
}
