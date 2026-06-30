import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {CreateScene, Scene, UpdateScene} from '../models/scene.model';

@Injectable({
  providedIn: 'root',
})
export class SceneApiService {
  private baseUrl = 'http://192.168.2.26:5001/api/Scene';
  private http = inject(HttpClient);

  getScenes() {
    return this.http.get<Scene[]>(`${this.baseUrl}`);
  }

  createScene(scene: CreateScene) {
    return this.http.post<Scene>(`${this.baseUrl}`, scene);
  }

  getScene(id: number) {
    return this.http.get<Scene>(`${this.baseUrl}/${id}`);
  }

  updateScene(id: number, scene: UpdateScene) {
    return this.http.put<Scene>(`${this.baseUrl}/${id}`, scene);
  }

  deleteScene(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

  scenePost(id: number, endpoint: string) {
    return this.http.post(`${this.baseUrl}/${id}/${endpoint}`, {});
  }

  disableScene(id: number) {
    return this.http.post(`${this.baseUrl}/${id}/disable`, {});
  }
  executeScene(id: number) {
    return this.http.post(`${this.baseUrl}/${id}/execute`, {});
  }
}
