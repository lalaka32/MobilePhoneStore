import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

const apiUrl: string = '/api/Auth';
@Injectable()
export class AuthenticationService {

  constructor(private http: HttpClient) { }

  registryUser(login: string, password: string) {

    return this.http.post<any>(apiUrl +'/Register', { login, password })
      .pipe(map(user => {
        if (user && user.token) {
          localStorage.setItem('TokenInfo', JSON.stringify(user));
        }

        return user;
      }));
  }

  login(login: string, password: string) {
    return this.http.post<any>(apiUrl +'/Login', { login, password })
      .pipe(map(user => {
        if (user && user.token) {
          localStorage.setItem('TokenInfo', JSON.stringify(user));
        }

        return user;
      }));
  }

  logout() {
    localStorage.removeItem('TokenInfo');
  }
}
