import {inject, Injectable} from '@angular/core';
import * as signalR from '@microsoft/signalr'
import {EntityStore} from '../stores/entity.store';

@Injectable({
  providedIn: 'root',
})
export class DeviceSignalrService {
  private baseUrl = 'http://localhost:5001';
  private hub?: signalR.HubConnection;
  private entityStore = inject(EntityStore);

  start() {
    this.hub = new signalR.HubConnectionBuilder()
      .withUrl(`${this.baseUrl}/hub`)
      .withAutomaticReconnect()
      .build()

    return this.hub.start();
  }

  onEntityUpdated() {
    this.hub?.on('EntityUpdated', entity => {
      console.log('Entity Updated', entity);
      this.entityStore.updateEntity(entity);
    });
  }
}
