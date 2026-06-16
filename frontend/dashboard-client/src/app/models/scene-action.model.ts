export interface SceneAction {
  id?: number;
  entityId: string;
  action: string;
  payload?: Record<string, any>;
}
