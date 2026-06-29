import {computed, Injectable, signal} from '@angular/core';
import {LoginApiService} from '../services/login-api.service';
import {JwtDecodeService} from '../services/jwt-decode.service';
import {Login} from '../models/login.model';

@Injectable({
  providedIn: 'root',
})
export class LoginStore {
  private _token = signal<string | null>(null);
  private _loading = signal<boolean | null>(null);
  private _error = signal<string | null>(null);

  private logoutTimer: any;

  isLoggedIn = computed(() => !!this._token());

  constructor(private loginApi: LoginApiService, private jwt: JwtDecodeService) {}

  init() {
    // localStorage.clear();
    const token = localStorage.getItem('authToken');
    console.log(this._token.toString())

    if (!token || this.jwt.isExpired(token)) {
      this.logout();
      return;
    }

    this.setToken(token);
  }

  setToken(token: string) {
    this._token.set(token);
    localStorage.setItem('authToken', token);

    this.startAutoLogout(token);
  }
  clearToken() {
    this._token.set(null);
    localStorage.clear();
  }

  login(credentials: Login) {
    this._loading.set(true);
    this._error.set(null);

    this.loginApi.postLogin(credentials).subscribe({
      next: (response) => {
        this.setToken(response.result);
        this._loading.set(false);
      },
      error: () => {
        this._error.set('Invalid username or password');
        this._loading.set(false);
      }
    })
  }

  logout() {
    this.clearToken();
  }

  private startAutoLogout(token: string) {
    const exp = this.jwt.getExpiration(token);
    if (!exp) return;

    const timeout = exp - Date.now();

    if (timeout <= 0) {
      this.logout();
      return;
    }

    clearTimeout(this.logoutTimer);

    this.logoutTimer = setTimeout(() => {
      this.logout();
    }, timeout);
  }
}
