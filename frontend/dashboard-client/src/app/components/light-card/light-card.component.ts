import {Component, inject, Input, signal} from '@angular/core';
import {Entity} from '../../models/entity.model';
import {DeviceStore} from '../../stores/device.store';

@Component({
  selector: 'app-light-card',
  imports: [],
  templateUrl: './light-card.component.html',
  styleUrl: './light-card.component.css',
})
export class LightCardComponent {
  @Input({required:true})
  entity!: Entity;

  private deviceStore = inject(DeviceStore);
  private _brightness = signal(1);
  brightness = this._brightness;

  ngOnInit() {
    this.updateBrightness()
  }

  ngOnChanges() {
    this.updateBrightness();
  }

  turnOn() {
    this.deviceStore.turnOn(this.entity)
  }

  turnOff() {
    this.deviceStore.turnOff(this.entity);
  }

  onSliderChange(event: Event) {
    this.deviceStore.onSliderChange(this.entity, event);
  }

  get isOn(): boolean {
    return this.entity.state === 'on';
  }

  updateBrightness() {
    const attribute = this.entity.attributes as Record<string, any>;

    this._brightness.set(attribute["brightness"] ?? 1);
  }
}
