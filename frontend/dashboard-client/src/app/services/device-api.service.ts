import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {DeviceCommand} from '../models/device-command.model';

@Injectable({
  providedIn: 'root',
})
export class DeviceApiService {
  private baseUrl = 'http://192.168.2.26:5001/api/Device';
  private http = inject(HttpClient);

  execute(command: DeviceCommand) {
    return this.http.post(`${this.baseUrl}`, command);
  }
}
