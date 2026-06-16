import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateSceneDialogComponent } from './create-scene-dialog.component';

describe('CreateSceneDialogComponent', () => {
  let component: CreateSceneDialogComponent;
  let fixture: ComponentFixture<CreateSceneDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateSceneDialogComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CreateSceneDialogComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
