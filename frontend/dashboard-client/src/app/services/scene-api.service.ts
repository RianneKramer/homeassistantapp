import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Scene} from '../models/scene.model';

@Injectable({
  providedIn: 'root',
})
export class SceneApiService {
  private baseUrl = 'http://localhost:5001';
  private http = inject(HttpClient);

  getScenes() {
    return this.http.get<Scene[]>(`${this.baseUrl}/api/Scene`);
  }

  createScene(scene: any) {
    return this.http.post<Scene>(`${this.baseUrl}/api/Scene`, scene);
  }

  getScene(id: number) {
    return this.http.get<Scene>(`${this.baseUrl}/api/Scenes/${id}`);
  }

  updateScene(scene: any, id: number) {
    return this.http.put<Scene>(`${this.baseUrl}/api/Scene/${id}`, scene);
  }

  deleteScene(id: number) {
    return this.http.delete(`${this.baseUrl}/api/Scene/${id}`);
  }

  enableScene(id: number) {
    return this.http.post(`${this.baseUrl}/api/Scene/${id}/enable`, {});
  }

  disableScene(id: number) {
    return this.http.post(`${this.baseUrl}/api/Scene/${id}/disable`, {});
  }
  executeScene(id: number) {
    return this.http.post(`${this.baseUrl}/api/Scene/${id}/execute`, {});
  }
}
