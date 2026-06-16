import {Component, computed, inject} from '@angular/core';
import {EntityStore} from '../../stores/entity.store';
import {SensorCardComponent} from '../../components/sensor-card/sensor-card.component';

@Component({
  selector: 'app-sensor-monitoring-page',
  imports: [
    SensorCardComponent
  ],
  templateUrl: './sensor-monitoring-page.component.html',
  styleUrl: './sensor-monitoring-page.component.css',
})
export class SensorMonitoringPageComponent {
  private entityStore = inject(EntityStore)
  sensors = computed(() =>
    this.entityStore.entities()
      .filter(entity =>
        entity.entity_id.startsWith('sensor.')))

  ngOnInit() {
    this.loadEntities();
  }

  loadEntities() {
    this.entityStore.loadEntities()
  }
}
