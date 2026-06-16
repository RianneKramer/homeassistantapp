import {Component, inject, signal} from '@angular/core';
import {SceneApiService} from '../../services/scene-api.service';
import {Scene} from '../../models/scene.model';
import {SceneCardComponent} from '../../components/scene-card/scene-card.component';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-scene-management-page',
  imports: [
    SceneCardComponent,
    RouterLink
  ],
  templateUrl: './scene-management-page.component.html',
  styleUrl: './scene-management-page.component.css',
})
export class SceneManagementPageComponent {
  private sceneApi = inject(SceneApiService)

  scenes = signal<Scene[]>([]);

  ngOnInit() {
    this.sceneApi.getScenes().subscribe(scenes => this.scenes.set(scenes));
  }

  deleteScene(id: number) {
    this.sceneApi.deleteScene(id).subscribe(() => {
      this.scenes.update(current => current.filter(x => x.id !== id));
    });
  }
}
