import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private hubConnection!: signalR.HubConnection;

  startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5001/lightHub')
      .withAutomaticReconnect()
      .build();
  }

  registerLightUpdates(callback: (data: any) => void) {
    this.hubConnection.on('LightUpdated', callback);
  }
}
