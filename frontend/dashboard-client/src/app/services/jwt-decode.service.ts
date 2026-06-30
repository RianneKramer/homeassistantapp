import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class JwtDecodeService {
  getExpiration(token: string) {
    if (!token) return null;

    const decoded: any = jwtDecode(token);
    return decoded.exp ? decoded.exp * 1000 : null;
  }

  isExpired(token: string) {
    const exp = this.getExpiration(token);
    return !exp || exp <= Date.now();
  }
}
