import {inject, Injectable} from '@angular/core';
import * as signalR from '@microsoft/signalr';
import {EntityStore} from '../stores/entity.store';
import {Entity} from '../models/entity.model';
import {EnergyStore} from '../stores/energy.store';

@Injectable({
  providedIn: 'root',
})
export class DeviceSignalrService {
  private baseUrl = 'http://localhost:5001';
  private hub?: signalR.HubConnection;
  private entityStore = inject(EntityStore);
  private energyStore = inject(EnergyStore);

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
      this.energyStore.loadOverview();
    });
  }
}
