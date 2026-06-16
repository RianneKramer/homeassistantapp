import {Component, signal} from '@angular/core';
import {form, FormField, required, submit} from '@angular/forms/signals';

interface Login {
  username: string;
  password: string;
}

@Component({
  selector: 'app-login-form',
  imports: [
    FormField
  ],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.css',
})
export class LoginFormComponent {
  loginModel = signal<Login>({
    username: '',
    password: '',
  })

  loginForm = form(this.loginModel, (fieldPath) => {
    required(fieldPath.username);
    required(fieldPath.password);
  })

  onSubmit(event: Event) {
    event.preventDefault();

    submit(this.loginForm);
  }
}
