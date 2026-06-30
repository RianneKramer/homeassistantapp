import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Entity} from '../models/entity.model';

@Injectable({
  providedIn: 'root',
})
export class EntityApiService {
  private baseUrl = 'http://192.168.2.26:5001/api/Entities';
  private http = inject(HttpClient);

  getEntities() {
    return this.http.get<Entity[]>(`${this.baseUrl}`)
  }
}
