import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {DeviceCommand} from '../models/device-command.model';

@Injectable({
  providedIn: 'root',
})
export class DeviceApiService {
  private baseUrl = 'http://localhost:5001';
  private http = inject(HttpClient);

  execute(command: DeviceCommand) {
    return this.http.post(`${this.baseUrl}/api/Device`, command);
  }
}
