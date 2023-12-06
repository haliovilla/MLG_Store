import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit{

  loginForm!: FormGroup;

  constructor(private authService: AuthService,
    private router:Router,
    private fb: FormBuilder) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  isValidForm(): boolean {
    return this.loginForm.valid;
  }

  login() {
if (this.isValidForm()) {
      this.authService.login(this.loginForm.value)
        .subscribe((response) => {
          if (response) {
            this.router.navigate(['home']);
          } else {
            alert("Invalid username or password");
          }
        });
    }
  }

}
