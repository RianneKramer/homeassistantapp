import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SensorMonitoringPageComponent } from './sensor-monitoring-page.component';

describe('SensorMonitoringPageComponent', () => {
  let component: SensorMonitoringPageComponent;
  let fixture: ComponentFixture<SensorMonitoringPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SensorMonitoringPageComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(SensorMonitoringPageComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
