import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActionEditorComponent } from './action-editor.component';

describe('ActionEditorComponent', () => {
  let component: ActionEditorComponent;
  let fixture: ComponentFixture<ActionEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ActionEditorComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ActionEditorComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
