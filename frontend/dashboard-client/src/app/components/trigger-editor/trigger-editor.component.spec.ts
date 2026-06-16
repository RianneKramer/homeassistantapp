import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TriggerEditorComponent } from './trigger-editor.component';

describe('TriggerEditorComponent', () => {
  let component: TriggerEditorComponent;
  let fixture: ComponentFixture<TriggerEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TriggerEditorComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(TriggerEditorComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
