import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { tap } from 'rxjs';
import { AccountService } from '../../../core/services/account-service';

@Component({
  selector: 'soc-register',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  heading = "Please register";
  registerForm: FormGroup;
  constructor(private accountService: AccountService, private fb: FormBuilder) {
    this.registerForm = fb.group({
      // name, email, password
      name: ['', [Validators.required]],
      email: ['', [Validators.required]],
      password: ['', [Validators.required]],
    })
  }

  register() {
    this.accountService.register(this.registerForm.value)
      .pipe(
        tap(x => console.log("api response: ", x))
      ).subscribe({
        next: (resp) => {
          console.log(resp)
        },
        error: (x) => (x)
      })
  }
}
