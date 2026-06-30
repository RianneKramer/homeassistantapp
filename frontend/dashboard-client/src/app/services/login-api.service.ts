import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Login, LoginResponse} from '../models/login.model';

@Injectable({
  providedIn: 'root',
})
export class LoginApiService {
  private baseUrl = 'http://localhost:5001/api/Login';
  private http = inject(HttpClient);

  postLogin(credentials: Login){
    return this.http.post<LoginResponse>(this.baseUrl, credentials)
  }
}
