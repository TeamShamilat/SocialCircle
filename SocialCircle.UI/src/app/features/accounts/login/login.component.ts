import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { tap } from 'rxjs';
import { AccountService } from '../../../core/services/account-service';
import { Router } from '@angular/router';

@Component({
  selector: 'soc-login',
  standalone: true, 
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
 
export class LoginComponent {
  isSubmitting = false;
  heading = "Login";
  loginForm: FormGroup;
  constructor(
    private accountService: AccountService,
    private router: Router,
     private fb: FormBuilder) {
    this.loginForm = fb.group({
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]],
    })
  }

  login() {
    this.isSubmitting = true;
    this.accountService.login(this.loginForm.value)
      .pipe(
        // tap(x => console.log("api response: ", x))
      ).subscribe({
        next: (resp) => { 
          this.isSubmitting = false;
          if(resp.token){
            // TODO: move to constants 'access_token' -> Mohsin
            localStorage.setItem('access_token', resp.token);
           this.router.navigate(['/home'])
          }
        },
        error: (x) => {
          this.isSubmitting = false;
        }
      })
    }
    
    navigateToRegister() {
     this.router.navigate(['/register'])
    }
}
