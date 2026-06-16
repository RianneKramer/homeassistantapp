import {SceneTrigger} from './scene-trigger.model';
import {SceneAction} from './scene-action.model';
import {TriggerType} from './trigger-type.enum';

export interface Scene {
  id: number;
  name: string;
  enabled: boolean;
  triggerType: TriggerType;
  triggerAt: string;
  runOnce: boolean;
  triggers: SceneTrigger[];
  actions: SceneAction[];
}
