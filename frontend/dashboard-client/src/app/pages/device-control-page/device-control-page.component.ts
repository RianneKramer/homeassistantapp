import {Component, inject} from '@angular/core';
import {Entity} from '../../models/entity.model';
import {EntityApiService} from '../../services/entity-api.service';
import {LightCardComponent} from '../../components/light-card/light-card.component';
import {NgIf} from '@angular/common';
import {EntityStore} from '../../stores/entity.store';
import {DeviceSignalrService} from '../../services/device-signalr.service';

@Component({
  selector: 'app-device-control-page',
  imports: [
    LightCardComponent,
  ],
  templateUrl: './device-control-page.component.html',
  styleUrl: './device-control-page.component.css',
})
export class DeviceControlPageComponent {
  private entityStore = inject(EntityStore)
  private signalr = inject(DeviceSignalrService)

  entities = this.entityStore.entities;

  ngOnInit() {
    this.loadEntities();
  }

  loadEntities() {
    this.entityStore.loadEntities();
  }
}
