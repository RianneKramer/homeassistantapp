import {Injectable, signal} from '@angular/core';
import {EnergyOverview} from '../models/energy-overview.model';
import {EnergyApiService} from '../services/energy-api.service';

@Injectable({
  providedIn: 'root',
})
export class EnergyStore {
  private _overview = signal<EnergyOverview | null>(null);

  private _loading = signal(false);
  private _error = signal<string | null>(null)

  overview = this._overview.asReadonly();

  constructor(private energyApi: EnergyApiService) {}

  loadOverview() {
    this._loading.set(true);
    this._error.set(null);

    this.energyApi.getOverview().subscribe({
      next: data => {
        this._overview.set(data);
        this._loading.set(false);
      },
      error: error => {
        this._error.set(error);
        this._loading.set(false);
      }
    })
  }
}
