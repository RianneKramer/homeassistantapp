import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SceneFormPageComponent } from './scene-form-page.component';

describe('CreateScenePageComponent', () => {
  let component: SceneFormPageComponent;
  let fixture: ComponentFixture<SceneFormPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SceneFormPageComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(SceneFormPageComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
