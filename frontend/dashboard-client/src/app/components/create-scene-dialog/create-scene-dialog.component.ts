import {Component, inject} from '@angular/core';
import {FormArray, FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import {TriggerType} from '../../models/trigger-type.enum';
import {SceneApiService} from '../../services/scene-api.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-create-scene-dialog',
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './create-scene-dialog.component.html',
  styleUrl: './create-scene-dialog.component.css',
})
export class CreateSceneDialogComponent {
  private fb = inject(FormBuilder);
  private router = inject(Router)
  private sceneApi = inject(SceneApiService)

  form = this.fb.group({
    name: ['', [Validators.required]],
    triggerType: [TriggerType.state],
    triggerAt: [null],
    runOnce: [true],
    triggers: this.fb.array([]),
    actions: this.fb.array([])
  });

  get triggers():FormArray {
    return this.form.controls.triggers;
  }

  get actions():FormArray {
    return this.form.controls.actions;
  }

  addTrigger() {
    this.triggers.push(
      this.fb.group({
        entityId: ['', [Validators.required]],
        operator: ['=='],
        value: ['']
      })
    );
  }

  addAction() {
    this.actions.push(
      this.fb.group({
        entityId: ['', [Validators.required]],
        action: ['turn_on']
      })
    )
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    console.log(this.form.getRawValue())

    this.sceneApi.createScene(this.form.getRawValue()).subscribe(() => this.router.navigate(['/scenes']));
  }
}
