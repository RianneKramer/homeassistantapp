import {Component, inject, signal} from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';
import {DeviceControlPageComponent} from './pages/device-control-page/device-control-page.component';
import {DeviceSignalrService} from './services/device-signalr.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('dashboard-client');

  private signalr = inject(DeviceSignalrService)

  ngOnInit() {
    this.signalr.start();
    this.signalr.onEntityUpdated();
  }
}
