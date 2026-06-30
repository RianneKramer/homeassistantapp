import {inject, Injectable, signal} from '@angular/core';
import {SettingsApiService} from '../services/settings-api.service';
import {Settings} from '../models/settings.model';

@Injectable({
  providedIn: 'root',
})
export class SettingsStore {
  private settingsApi = inject(SettingsApiService)

  private _loading = signal<boolean | null>(null)
  private _error = signal<string | null>(null)

  private _currentSettings = signal<Settings | null>(null);
  private _connectionStatus = signal<boolean | null>(null);

  private _settingsModel = signal<Settings>({
    homeAssistantUrl: '',
    homeAssistantToken: '',
  })

  settingsModel = this._settingsModel;
  currentSettings = this._currentSettings.asReadonly();
  connectionStatus = this._connectionStatus.asReadonly();

  init() {
    this._loading.set(true);
    this._error.set(null)

    this.settingsApi.getSettings().subscribe({
      next: settings => {
        this._currentSettings.set(settings);
        this._settingsModel.set(settings);
        this._loading.set(false);
      },
      error: err => {
        this._error.set(err);
        this._loading.set(false);
      }
    })
  }

  testConnection() {
    this._loading.set(true);
    this._error.set(null)

    const settings = this.settingsModel();

    this.settingsApi.testConnection(settings).subscribe({
      next: connected => {
        this._connectionStatus.set(connected);
        this._loading.set(false);
      },
      error: err => {
        this._connectionStatus.set(false);
        this._error.set(err);
        this._loading.set(false);
      }
    })
  }

  save() {
    this._loading.set(true);

    const settings = this.settingsModel();

    this.settingsApi.updateSettings(settings).subscribe(() => {
      this._currentSettings.set(settings);
      this._loading.set(false);
    })
  }
}
