import {Component, inject} from '@angular/core';
import {form, FormField, required} from '@angular/forms/signals';
import {SettingsStore} from '../../stores/settings.store';

@Component({
  selector: 'app-settings-form',
  imports: [
    FormField
  ],
  templateUrl: './settings-form.component.html',
  styleUrl: './settings-form.component.css',
})
export class SettingsFormComponent {
  private settingsStore = inject(SettingsStore);

  settingsModel = this.settingsStore.settingsModel;

  settingsForm = form(this.settingsModel, (fieldPath) => {
    required(fieldPath.homeAssistantUrl, {message: 'Enter home assistant url'});
    required(fieldPath.homeAssistantToken, {message: 'Enter home assistant token'});
  });

  currentSettings = this.settingsStore.currentSettings;
  connectionStatus = this.settingsStore.connectionStatus;

  ngOnInit() {
    this.settingsStore.init()
  }

  testConnection() {
    this.settingsStore.testConnection()
  }

  save() {
    this.settingsStore.save();
  }
}
