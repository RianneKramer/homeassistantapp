import {Component} from '@angular/core';
import {CreateSceneDialogComponent} from '../../components/create-scene-dialog/create-scene-dialog.component';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-scene-form-page',
  imports: [
    CreateSceneDialogComponent,
    FormsModule
  ],
  templateUrl: './scene-form-page.component.html',
  styleUrl: './scene-form-page.component.css',
})
export class SceneFormPageComponent {}
