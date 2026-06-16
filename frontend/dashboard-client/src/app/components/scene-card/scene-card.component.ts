import {Component, inject, Input} from '@angular/core';
import {SceneApiService} from '../../services/scene-api.service';
import {Scene} from '../../models/scene.model';

@Component({
  selector: 'app-scene-card',
  imports: [],
  templateUrl: './scene-card.component.html',
  styleUrl: './scene-card.component.css',
})
export class SceneCardComponent {
  @Input({required:true})
  scene!: Scene;

  private api = inject(SceneApiService)

  toggle() {
    if (this.scene.enabled){
      this.api.disableScene(this.scene.id).subscribe(() => this.scene.enabled = false);
    }
    else {
      this.api.enableScene(this.scene.id).subscribe(() => this.scene.enabled = true);
    }
  }

  execute(){
    this.api.executeScene(this.scene.id).subscribe();
  }
}
