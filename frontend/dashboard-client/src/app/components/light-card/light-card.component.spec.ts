import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LightCardComponent } from './light-card.component';

describe('LightCardComponent', () => {
  let component: LightCardComponent;
  let fixture: ComponentFixture<LightCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LightCardComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(LightCardComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
