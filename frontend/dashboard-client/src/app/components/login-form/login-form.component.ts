import {Component, effect, inject, signal} from '@angular/core';
import {form, FormField, required, submit} from '@angular/forms/signals';
import {LoginStore} from '../../stores/login.store';
import {Router} from '@angular/router';

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
  private loginStore = inject(LoginStore);
  private router = inject(Router);

  constructor() {
    effect(() => {
      if (this.loginStore.isLoggedIn()) {
        console.log(this.loginStore.isLoggedIn())
        this.router.navigate(['/devices']);
      }
    });
  }

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

    submit(this.loginForm, async () => {
      this.loginStore.login(this.loginModel());
    });
  }
}
