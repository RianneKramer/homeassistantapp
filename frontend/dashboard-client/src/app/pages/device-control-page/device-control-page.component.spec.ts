import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeviceControlPageComponent } from './device-control-page.component';

describe('DeviceControlPageComponent', () => {
  let component: DeviceControlPageComponent;
  let fixture: ComponentFixture<DeviceControlPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeviceControlPageComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(DeviceControlPageComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
