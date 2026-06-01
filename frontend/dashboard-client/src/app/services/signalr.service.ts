import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private hubConnection!: signalR.HubConnection;

  startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5001/hub')
      .withAutomaticReconnect()
      .build();

    this.registerListeners()

    return this.hubConnection.start();
  }

  registerListeners() {
    this.hubConnection.on('LightUpdated', (light) => {
      console.log(light);
    })
  }
}
