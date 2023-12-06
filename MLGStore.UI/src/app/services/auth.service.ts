import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { environment } from '../../environment';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ErrorService } from './error.service';
import { LoginRequestDTO } from '../models/login';
import { Result } from '../models/result';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private sessionTokenSubject: BehaviorSubject<string> | undefined;
  public sessionToken$!: Observable<string>;

  apiUrl: string = `${environment.apiUrlBase}/Account`;;

  constructor(private http: HttpClient,
    private errorService: ErrorService) {
    this.sessionTokenSubject = new BehaviorSubject<string>('');
    if (localStorage.getItem("token")!=null) {
      this.sessionTokenSubject.next(localStorage.getItem("token") as string);
    }
  }

  public sessionTokenValue(): string | undefined {
    return this.sessionTokenSubject?.value;
  }

  getToken = () => this.sessionTokenSubject?.asObservable();

  login(loginRequest: LoginRequestDTO) {
    const uri = `${this.apiUrl}/Login`;;
    return this.http.post<Result<string>>(uri, loginRequest)
      .pipe(map((response: Result<string>) => {
        if (response.success && response.data && response.data !== '') {
          localStorage.setItem("token", response.data);
          this.sessionTokenSubject?.next(response.data);
          return true;
        }
        this.errorService.logError(response);
        return false;
      }));
  }

  logout() {
    localStorage.removeItem("token");
    this.sessionTokenSubject?.next(null as any);
  }
}
