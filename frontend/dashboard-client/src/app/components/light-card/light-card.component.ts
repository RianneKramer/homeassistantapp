import {Component, inject, Input} from '@angular/core';
import {Entity} from '../../models/entity.model';
import {DeviceApiService} from '../../services/device-api.service';

@Component({
  selector: 'app-light-card',
  imports: [],
  templateUrl: './light-card.component.html',
  styleUrl: './light-card.component.css',
})
export class LightCardComponent {
  @Input({required:true})
  entity!: Entity;

  brightness = 1

  private deviceApi = inject(DeviceApiService);
  private brightnessTimeout?: number;

  ngOnInit() {
    if (!this.entity.attributes){
      console.error("no attributes");
      return;
    }
    try {
      const attribute: Record<string, any> = this.entity.attributes;
      console.log(attribute["brightness"]);

      this.brightness = attribute["brightness"] ?? 1;
    }
    catch {
      console.error('Unable to parse brightness', this);
      this.brightness = 1
    }
  }

  turnOn() {
    this.deviceApi.execute({
      entityId: this.entity.entity_id,
      action: 'turn_on'
    }).subscribe();
  }

  turnOff() {
    this.deviceApi.execute({
      entityId: this.entity.entity_id,
      action: 'turn_off'
    }).subscribe();
  }

  onSliderChange(event: Event) {
    const brightness = Number(
      (event.target as HTMLInputElement).value
    );

    this.brightness = brightness;

    clearTimeout(this.brightnessTimeout);

    this.brightnessTimeout = window.setTimeout(() => {
      this.deviceApi.execute({
        entityId: this.entity.entity_id,
        action: 'turn_on',
        data: { 'brightness': brightness}
      }).subscribe();
    }, 250);
  }

  get isOn(): boolean {
    return this.entity.state === 'on';
  }
}
