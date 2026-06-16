export interface Entity {
  id: number;
  entity_id: string;
  name: string;
  state: string;
  attributes: Record<string, any>;
}
