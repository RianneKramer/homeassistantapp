import { Component } from '@angular/core';
import {LightDimmerComponent} from './light-dimmer/light-dimmer.component';

@Component({
  selector: 'app-dashboard',
  imports: [
    LightDimmerComponent
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
})
export class DashboardComponent {
  lights = [
    { id: 1, name: 'Woonkamer', brightness: 75, isOn: true},
    { id: 2, name: 'Keuken', brightness: 40, isOn: false},
    { id: 3, name: 'Slaapkamer', brightness: 20, isOn: true},
  ]
}
