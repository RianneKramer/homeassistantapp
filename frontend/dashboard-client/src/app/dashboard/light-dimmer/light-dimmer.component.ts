import {Component, Input} from '@angular/core';
import {FormsModule} from '@angular/forms';

export interface LightGroup {
  id: number;
  name: string;
  brightness: number;
  isOn: boolean;
}

@Component({
  selector: 'app-light-dimmer',
  imports: [
    FormsModule
  ],
  templateUrl: './light-dimmer.component.html',
  styleUrl: './light-dimmer.component.css',
})
export class LightDimmerComponent {
  @Input() light!: LightGroup;

  toggleLight() {
    this.light.isOn = !this.light.isOn;
  }


}
