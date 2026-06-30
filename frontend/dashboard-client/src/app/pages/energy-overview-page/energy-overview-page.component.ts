import {Component, inject} from '@angular/core';
import {EnergyStore} from '../../stores/energy.store';

@Component({
  selector: 'app-energy-overview-page',
  imports: [],
  templateUrl: './energy-overview-page.component.html',
  styleUrl: './energy-overview-page.component.css',
})
export class EnergyOverviewPageComponent {
  private store = inject(EnergyStore)

  overview = this.store.overview;

  ngOnInit() {
    this.store.loadOverview();
  }
}
