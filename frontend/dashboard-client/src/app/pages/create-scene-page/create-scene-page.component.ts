import { Component } from '@angular/core';
import {CreateSceneDialogComponent} from '../../components/create-scene-dialog/create-scene-dialog.component';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-create-scene-page',
  imports: [
    CreateSceneDialogComponent,
    FormsModule
  ],
  templateUrl: './create-scene-page.component.html',
  styleUrl: './create-scene-page.component.css',
})
export class CreateScenePageComponent {}
