import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { tap } from 'rxjs';

@Component({
  selector: 'soc-register',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    // ReactiveFormsModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  constructor(private http: HttpClient){

  }
  heading = "Please register";

  data: any = {};

  register() {
    const url = 'http://localhost:5550/api/Accounts/Register'
    // console.log("submitting form: ", this.data);
    // TODO: validation check here.

    this.http.post(url, this.data)
    .pipe(
      // rjxs
      tap(x => console.log("api response: ", x))
    ).subscribe({
      next: (resp) => {
        console.log(resp)
      },
      error: (x) => (x)
    })
  }
}
