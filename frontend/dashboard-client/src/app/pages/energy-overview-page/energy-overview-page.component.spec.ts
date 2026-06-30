import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnergyOverviewPageComponent } from './energy-overview-page.component';

describe('EnergyOverviewPageComponent', () => {
  let component: EnergyOverviewPageComponent;
  let fixture: ComponentFixture<EnergyOverviewPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EnergyOverviewPageComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(EnergyOverviewPageComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
