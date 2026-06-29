import {inject, Injectable, signal} from '@angular/core';
import {DeviceApiService} from '../services/device-api.service';
import {Entity} from '../models/entity.model';

@Injectable({
  providedIn: 'root',
})
export class DeviceStore {
  private deviceApi = inject(DeviceApiService);

  private _loading = signal<boolean | null>(null);
  private _error = signal<string | null>(null);

  // private _brightness= signal(1);
  private _brightnessTimeout?: number;

  turnOn(entity: Entity) {
    this._loading.set(true);
    this._error.set(null);

    this.deviceApi.execute({
      entityId: entity.entity_id,
      action: 'turn_on'
    }).subscribe({
      next: () => this._loading.set(false),
      error: () => {
        this._loading.set(false);
        this._error.set('Failed to turn on');
      }
    });
  }

  turnOff(entity: Entity) {
    this._loading.set(true);
    this._error.set(null);

    this.deviceApi.execute({
      entityId: entity.entity_id,
      action: 'turn_off'
    }).subscribe({
      next: () => this._loading.set(false),
      error: () => {
        this._loading.set(false);
        this._error.set('Failed to turn off');
      }
    });
  }

  onSliderChange(entity: Entity, event: Event) {
    this._loading.set(true);
    this._error.set(null);

    const brightness = Number(
      (event.target as HTMLInputElement).value
    );

    // this._brightness.set(brightness);

    clearTimeout(this._brightnessTimeout);

    this._brightnessTimeout = window.setTimeout(() => {
      this.deviceApi.execute({
        entityId: entity.entity_id,
        action: 'turn_on',
        data: {
          'brightness': brightness
        }
      }).subscribe();
    }, 250);
  }
}
