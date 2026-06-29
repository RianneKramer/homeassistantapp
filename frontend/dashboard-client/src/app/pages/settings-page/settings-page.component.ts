import {Component, Inject, signal} from '@angular/core';
import {SettingsFormComponent} from '../../components/settings-form/settings-form.component';
import {Settings} from '../../models/settings.model';
import {SettingsApiService} from '../../services/settings-api.service';

@Component({
  selector: 'app-settings-page',
  imports: [
    SettingsFormComponent
  ],
  templateUrl: './settings-page.component.html',
  styleUrl: './settings-page.component.css',
})
export class SettingsPageComponent {
  private settingsApi = Inject(SettingsApiService);
}
