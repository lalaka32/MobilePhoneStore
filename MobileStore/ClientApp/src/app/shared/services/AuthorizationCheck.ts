import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable, Subject } from 'rxjs';

@Injectable()
export class AuthorizationCheck implements CanActivate {

  isAuthrize: boolean;

  public boolChanged: Subject<boolean> = new Subject<boolean>();

  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.hasToken()) {
      this.isAuthrize = true;
    }
    else {
      this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
      this.isAuthrize = false;
    }

   
    this.boolChanged.next(this.isAuthrize);
    return this.isAuthrize;
  }
  hasToken() {
    return localStorage.getItem('TokenInfo') != null;
  }
}
