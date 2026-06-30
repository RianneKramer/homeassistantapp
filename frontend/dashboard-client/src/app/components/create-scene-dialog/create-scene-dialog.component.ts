import {Component, inject} from '@angular/core';
import {TriggerType} from '../../models/trigger-type.enum';
import {form, FormField, required} from '@angular/forms/signals';
import {FormsModule} from '@angular/forms';
import {EntityStore} from '../../stores/entity.store';
import {SceneTrigger} from '../../models/scene-trigger.model';
import {SceneAction} from '../../models/scene-action.model';
import {SceneStore} from '../../stores/scene.store';

@Component({
  selector: 'app-create-scene-dialog',
  imports: [
    FormField,
    FormsModule
  ],
  templateUrl: './create-scene-dialog.component.html',
  styleUrl: './create-scene-dialog.component.css',
  providers: [SceneStore]
})
export class CreateSceneDialogComponent {
  private sceneStore = inject(SceneStore)
  private entityStore = inject(EntityStore);

  entities = this.entityStore.entities;
  isEditMode = this.sceneStore.isEditMode;

  ngOnInit() {
    this.sceneStore.init();
  }

  sceneModel = this.sceneStore.sceneModel;

  sceneForm = form(this.sceneModel, (fieldPath) => {
    required(fieldPath.name);
    required(fieldPath.triggerType);
    required(fieldPath.actions);
  })

  addTrigger() {
    this.sceneStore.addTrigger();
  }

  addAction() {
    this.sceneStore.addAction();
  }

  removeTrigger(index: number) {
    this.sceneStore.removeTrigger(index);
  }

  removeAction(index: number) {
    this.sceneStore.removeAction(index);
  }

  updateTrigger(index: number, field: keyof SceneTrigger, value: any) {
    this.sceneStore.updateTrigger(index, field, value);
  }

  updateAction(index: number, field: keyof SceneAction, value: any) {
    this.sceneStore.updateAction(index, field, value);
  }

  onTriggerTypeChanged(event: Event) {
    this.sceneStore.onTriggerTypeChanged(event);
  }

  saveCreate() {
    this.sceneStore.saveCreate();
  }

  saveUpdate() {
    this.sceneStore.saveUpdate();
  }

  protected readonly TriggerType = TriggerType;
}
