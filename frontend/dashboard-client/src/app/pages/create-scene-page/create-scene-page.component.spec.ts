import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateScenePageComponent } from './create-scene-page.component';

describe('CreateScenePageComponent', () => {
  let component: CreateScenePageComponent;
  let fixture: ComponentFixture<CreateScenePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateScenePageComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CreateScenePageComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
