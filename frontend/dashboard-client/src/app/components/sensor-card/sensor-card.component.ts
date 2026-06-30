import {Component, Input} from '@angular/core';
import {Entity} from '../../models/entity.model';

@Component({
  selector: 'app-sensor-card',
  imports: [],
  templateUrl: './sensor-card.component.html',
  styleUrl: './sensor-card.component.css',
})
export class SensorCardComponent {
  @Input({required:true})
  entity!: Entity;

  get value(): string {
    return this.entity.state;
  }

  get unit(): string {
    return this.entity.attributes['unit_of_measurement'] ?? '';
  }
}
