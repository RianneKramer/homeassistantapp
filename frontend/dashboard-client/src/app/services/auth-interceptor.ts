import { HttpInterceptorFn } from '@angular/common/http';
import {catchError, throwError} from 'rxjs';
import {inject} from '@angular/core';
import {LoginStore} from '../stores/login.store';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const token = localStorage.getItem('authToken');
  const loginStore = inject(LoginStore)

  if (token) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    })
  }
  return next(req).pipe(
    catchError((err) => {
      if (err.status === 401) {
        loginStore.logout()
      }
      return throwError(() => err);
    })
  );
};
