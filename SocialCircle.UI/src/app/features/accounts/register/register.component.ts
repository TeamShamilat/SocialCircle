import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'soc-register',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
heading  = "Please register";

data: any = {};
}
