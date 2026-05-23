import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private baseUrl = 'http://localhost:5001';
  private http = inject(HttpClient);

  postWebhook<T>(webhookUrl: string, query: string) {
    return this.http.get<T>(`${this.baseUrl}/webhooks/${webhookUrl}`);
  }
}
