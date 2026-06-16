import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: 'devices',
    loadComponent: () =>
      import('./pages/device-control-page/device-control-page.component')
        .then(m => m.DeviceControlPageComponent)
  },
  {
    path: 'sensors',
    loadComponent: () =>
      import('./pages/sensor-monitoring-page/sensor-monitoring-page.component')
        .then(m => m.SensorMonitoringPageComponent)
  },
  {
    path: 'scenes',
    loadComponent: () =>
      import('./pages/scene-management-page/scene-management-page.component')
        .then(m => m.SceneManagementPageComponent)
  },
  {
    path: 'scenes/create',
    loadComponent: () =>
      import('./pages/create-scene-page/create-scene-page.component')
        .then(m => m.CreateScenePageComponent)
  },
  {
    path: 'energy',
    loadComponent: () =>
      import('./pages/energy-overview-page/energy-overview-page.component')
        .then(m => m.EnergyOverviewPageComponent)
  },
  {
    path: 'settings',
    loadComponent: () =>
      import('./pages/settings-page/settings-page.component')
        .then(m => m.SettingsPageComponent)
  },
  {
    path: 'login',
    loadComponent: () =>
      import('./pages/login-page/login-page.component')
        .then(m => m.LoginPageComponent)
  },
  {
    path: '**',
    redirectTo: 'devices',
  }
];
