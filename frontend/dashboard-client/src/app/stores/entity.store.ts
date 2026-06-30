import {Injectable, signal} from '@angular/core';
import {Entity} from '../models/entity.model';
import {EntityApiService} from '../services/entity-api.service';

@Injectable({
  providedIn: 'root',
})
export class EntityStore {
  private _entities = signal<Entity[] | []>([]);

  private _loading = signal(false);
  private _error = signal<string | null>(null);

  loading = this._loading.asReadonly();
  error = this._error.asReadonly();

  entities = this._entities.asReadonly();

  constructor(private entityApi: EntityApiService) {}

  loadEntities(){
    this._loading.set(true);
    this._error.set(null);

    this.entityApi.getEntities().subscribe({
      next: (entities) => {
        this._entities.set(entities);
        this._loading.set(false);
      },
      error: (error) => {
        this._error.set('Failed to get entities');
        this._loading.set(false);
      }
    })
  }

  updateEntity(update: Entity) {
    this._entities.update(current =>
      current.map(entity =>
        entity.entity_id === update.entity_id ? {
          ...entity,
          state: update.state,
          attributes: update.attributes
        }
        : entity
      )
    );
  }
}
