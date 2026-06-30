import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Settings} from '../models/settings.model';

@Injectable({
  providedIn: 'root',
})
export class SettingsApiService {
  private baseUrl = 'http://localhost:5001/api/Settings';
  private http = inject(HttpClient);

  getSettings() {
    return this.http.get<Settings>(this.baseUrl);
  }

  updateSettings(settings: Settings) {
    return this.http.put(this.baseUrl , settings);
  }

  testConnection(settings: Settings) {
    return this.http.post<boolean>(`${this.baseUrl}/test`, settings);
  }
}
