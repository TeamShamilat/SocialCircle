import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { tap } from 'rxjs';
import { AccountService } from '../../../core/services/account-service';



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
  heading = "Login";
  loginForm: FormGroup;
  constructor(private accountService: AccountService, private fb: FormBuilder) {
    this.loginForm = fb.group({
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]],
    })
  }

  login() {
    this.accountService.login(this.loginForm.value)
      .pipe(
        tap(x => console.log("api response: ", x))
      ).subscribe({
        next: (resp) => { 
          console.log(resp);
        },
        error: (x) => (x)
      })
  }
}
