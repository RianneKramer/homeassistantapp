import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LightDimmerComponent } from './light-dimmer.component';

describe('LightDimmerComponent', () => {
  let component: LightDimmerComponent;
  let fixture: ComponentFixture<LightDimmerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LightDimmerComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(LightDimmerComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
