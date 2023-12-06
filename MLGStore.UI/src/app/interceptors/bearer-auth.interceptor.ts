import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable()
export class BearerAuthInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) { }

  intercept(request: HttpRequest<any>,
    next: HttpHandler): Observable<HttpEvent<any>> {

    request = request.clone({
      setHeaders: {
        'content-type': 'application/json'
      }
    });

    const token: string | undefined = this.authService.sessionTokenValue();
    if (token && token != undefined) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      }); 
    }

    return next.handle(request);

  }
}
