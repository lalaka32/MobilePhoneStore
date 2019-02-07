import { Component } from '@angular/core';
import { AuthorizationCheck } from '../shared/services/AuthorizationCheck';
import { AuthenticationService } from '../shared/services/AuthService';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  isAuthorized: boolean;
    _subscription: any;

  constructor(private authorizationCheckService: AuthorizationCheck, private authService: AuthenticationService) {
    this.isAuthorized = authorizationCheckService.isAuthrize;
    this._subscription = authorizationCheckService.boolChanged.subscribe((value) => {
      this.isAuthorized = value;
    });
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  logout() {
    this.authService.logout();
    window.location.reload();
  }
}
