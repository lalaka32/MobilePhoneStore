import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CommonModule } from "@angular/common";

import { MobilePhoneService } from './shared/services/MobilePhoneService';
import { AuthenticationService } from './shared/services/AuthService';
import { AuthorizationCheck } from './shared/services/AuthorizationCheck';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { PhoneListComponent } from './mobilePhone/phone-list/phone-list.component';
import { PhoneCatalogComponent } from './mobilePhone/phone-catalog/phone-catalog.component';
import { PhoneInfoComponent } from './mobilePhone/phone-info/phone-info.component';
import { PhoneFavoriteListComponent } from './mobilePhone/phone-favorite/phone-favourite.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';

import { httpInterceptor } from './shared/interceptor/httpInterceptor';
import { ErrorInterceptor } from './shared/interceptor/errorInterceptor';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    PhoneListComponent,
    PhoneInfoComponent,
    PhoneCatalogComponent,
    PhoneFavoriteListComponent,
    LoginComponent,
    RegistrationComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'catalog', component: PhoneCatalogComponent, canActivate: [AuthorizationCheck] },
      { path: 'info/:id', component: PhoneInfoComponent, canActivate: [AuthorizationCheck] },
      { path: 'favourite', component: PhoneFavoriteListComponent, canActivate: [AuthorizationCheck] },
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: httpInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    MobilePhoneService, AuthenticationService, AuthorizationCheck],
  bootstrap: [AppComponent]
})
export class AppModule { }
