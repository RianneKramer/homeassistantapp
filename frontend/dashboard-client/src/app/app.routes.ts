import { Routes } from '@angular/router';
import {LoginPageComponent} from './pages/login-page/login-page.component';
import {DeviceControlPageComponent} from './pages/device-control-page/device-control-page.component';
import {SensorMonitoringPageComponent} from './pages/sensor-monitoring-page/sensor-monitoring-page.component';
import {SceneManagementPageComponent} from './pages/scene-management-page/scene-management-page.component';
import {SceneFormPageComponent} from './pages/scene-form-page/scene-form-page.component';
import {EnergyOverviewPageComponent} from './pages/energy-overview-page/energy-overview-page.component';
import {SettingsPageComponent} from './pages/settings-page/settings-page.component';
import {authGuard} from './services/auth-guard';

export const routes: Routes = [
  {
    path: 'devices', component: DeviceControlPageComponent, canActivate: [authGuard]
  },
  {
    path: 'sensors', component: SensorMonitoringPageComponent, canActivate: [authGuard]
  },
  {
    path: 'scenes', component: SceneManagementPageComponent, canActivate: [authGuard]
  },
  {
    path: 'scenes/create', component: SceneFormPageComponent, canActivate: [authGuard]
  },
  {
    path: 'scenes/:id/edit', component: SceneFormPageComponent, canActivate: [authGuard]
  },
  {
    path: 'energy', component: EnergyOverviewPageComponent, canActivate: [authGuard]
  },
  {
    path: 'settings', component: SettingsPageComponent, canActivate: [authGuard]
  },
  {
    path: 'login', component: LoginPageComponent
  },
  {
    path: '**', redirectTo: 'login'
  }
];
