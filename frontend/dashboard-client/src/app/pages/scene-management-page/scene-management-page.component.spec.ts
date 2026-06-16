import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SceneManagementPageComponent } from './scene-management-page.component';

describe('SceneManagementPageComponent', () => {
  let component: SceneManagementPageComponent;
  let fixture: ComponentFixture<SceneManagementPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SceneManagementPageComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(SceneManagementPageComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
