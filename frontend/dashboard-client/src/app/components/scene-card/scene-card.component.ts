import {Component, inject, Input} from '@angular/core';
import {SceneApiService} from '../../services/scene-api.service';
import {Scene} from '../../models/scene.model';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-scene-card',
  imports: [
    RouterLink
  ],
  templateUrl: './scene-card.component.html',
  styleUrl: './scene-card.component.css',
})
export class SceneCardComponent {
  @Input({required:true})
  scene!: Scene;

  private api = inject(SceneApiService)

  toggle() {
    if (this.scene.enabled){
      this.api.scenePost(this.scene.id, 'disable').subscribe(() => this.scene.enabled = false);
    }
    else {
      this.api.scenePost(this.scene.id, 'enable').subscribe(() => this.scene.enabled = true);
    }
  }

  execute(){
    this.api.scenePost(this.scene.id, 'execute').subscribe();
  }

  delete() {
    this.api.deleteScene(this.scene.id).subscribe();
  }
}
