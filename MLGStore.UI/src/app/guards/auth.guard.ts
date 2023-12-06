import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  if (localStorage.getItem("token") != null) {
    const token: string = localStorage.getItem("token") as string;
    if (token != null && token !== '' ) {
      return true;
    }
    inject(Router).navigate(['/home/login']);
    return false;
  }
  inject(Router).navigate(['/home/login']);
  return false;
};
