import {inject, Injectable, signal} from '@angular/core';
import {SceneApiService} from '../services/scene-api.service';
import {EntityStore} from './entity.store';
import {ActivatedRoute, Router} from '@angular/router';
import {CreateScene, Scene, UpdateScene} from '../models/scene.model';
import {TriggerType} from '../models/trigger-type.enum';
import {SceneTrigger} from '../models/scene-trigger.model';
import {SceneAction} from '../models/scene-action.model';

@Injectable()

export class SceneStore {
  private entityStore = inject(EntityStore);
  private sceneApi = inject(SceneApiService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  private _sceneId = signal<number | null>(null);
  private _isEditMode = signal(false);

  private _sceneModel = signal<UpdateScene>({
    name: '',
    enabled: true,
    triggerType: TriggerType.state,
    triggerAt: null,
    runOnce: false,
    triggers: [],
    actions: [],
  });

  private _loading = signal<boolean | null>(null);
  private _error = signal<string | null>(null);

  sceneModel = this._sceneModel;
  isEditMode = this._isEditMode.asReadonly();

  init() {
    this._loading.set(true);
    this._error.set(null);

    this._sceneModel.set({
      name: '',
      enabled: true,
      triggerType: TriggerType.state,
      triggerAt: null,
      runOnce: false,
      triggers: [],
      actions: []
    });

    this._sceneId.set(null);
    this._isEditMode.set(false);

    this.entityStore.loadEntities();

    const id = this.route.snapshot.params['id'];

    if (!id) {
      this._loading.set(false);
      return;
    }

    this._sceneId.set(+id);
    this._isEditMode.set(true);

    this.sceneApi.getScene(+id).subscribe({
      next: scene => {
        this.loadScene(scene);
        this._loading.set(false);
      },
      error: error => {
        this._error.set(error);
        this._loading.set(false);
      }
    });
  }

  loadScene(scene: Scene) {
    this._sceneModel.set({
      name: scene.name,
      enabled: scene.enabled,
      triggerType: scene.triggerType,
      triggerAt: scene.triggerAt,
      runOnce: scene.runOnce,
      triggers: [...scene.triggers],
      actions: [...scene.actions]
    });
  }

  addTrigger() {
    this.sceneModel.update(scene => ({
      ...scene,

      triggers: [
        ...scene.triggers,
        {
          entityId: '',
          operator: '==',
          value: ''
        }
      ]
    }));
  }

  addAction() {
    this.sceneModel.update(scene => ({
      ...scene,

      actions: [
        ...scene.actions,
        {
          entityId: '',
          action: 'turn_on',
          payload: {}
        }
      ]
    }));
  }

  removeTrigger(index: number) {
    this.sceneModel.update(scene => ({
      ...scene,
      triggers: scene.triggers.filter((_, i) => i !== index)
    }));
  }

  removeAction(index: number) {
    this.sceneModel.update(scene => ({
      ...scene,
      actions: scene.actions.filter((_, i) => i !== index)
    }))
  }

  updateTrigger(index: number, field: keyof SceneTrigger, value: any) {
    this.sceneModel.update(scene => {
      const triggers = [...scene.triggers];
      triggers[index] = {
        ...triggers[index],
        [field]: value
      };

      return { ...scene, triggers };
    })
  }

  updateAction(index: number, field: keyof SceneAction, value: any) {
    this.sceneModel.update(scene => {

      const actions = [...scene.actions];

      actions[index] = {
        ...actions[index],
        [field]: value
      };

      return {
        ...scene,
        actions
      };
    });
  }

  onTriggerTypeChanged(event: Event) {

    const value = Number(
      (event.target as HTMLSelectElement).value
    ) as TriggerType;

    this.sceneModel.update(scene => ({
      ...scene,
      triggerType: value
    }));
  }

  saveCreate() {
    this._loading.set(true);
    this._error.set(null);

    const scene = this.sceneModel();

    const dto: CreateScene = {
      name: scene.name,
      triggerType: scene.triggerType,
      triggerAt: scene.triggerAt ? new Date(scene.triggerAt).toISOString() : null,
      runOnce: scene.runOnce,
      triggers: scene.triggers,
      actions: scene.actions
    };

    this.sceneApi.createScene(dto).subscribe({
      next: () => {
        this.router.navigate(['/scenes']);
        this._loading.set(false);
      },
      error: err => {
        this._error.set(err);
        this._loading.set(false);
      }
    });
  }

  saveUpdate() {
    this._loading.set(true);
    this._error.set(null);

    this.sceneApi.updateScene(this._sceneId()!, this.sceneModel()).subscribe({
      next: () => {
        this.router.navigate(['/scenes']);
        this._loading.set(false);
      },
      error: err => {
        this._error.set(err);
        this._loading.set(false);
      }
    });
  }
}
