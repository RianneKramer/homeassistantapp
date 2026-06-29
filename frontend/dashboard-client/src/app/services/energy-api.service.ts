import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {EnergyOverview} from '../models/energy-overview.model';

@Injectable({
  providedIn: 'root',
})
export class EnergyApiService {
  private baseUrl = 'http://localhost:5001/api/Energy';
  private http = inject(HttpClient);

  getOverview(){
    return this.http.get<EnergyOverview>(`${this.baseUrl}`);
  }
}
