import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AuthenticationService } from '../shared/services/AuthService';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
})
export class RegistrationComponent implements OnInit {
  registrationForm: FormGroup;
  submitClick = false;
  submitted = false;
  returnUrl: string;
  error ='';

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.registrationForm = this.formBuilder.group({
      login: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.authenticationService.logout();

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  get formData() { return this.registrationForm.controls; }

  onLogin() {
    this.submitted = true;

    if (this.registrationForm.invalid) {
      return;
    }

    this.submitClick = true;
    this.authenticationService.registryUser(this.formData.login.value, this.formData.password.value)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate([this.returnUrl]);
        },
      error => {
          this.error = error.error.error;
          this.submitClick = false;
        });
  }
}
