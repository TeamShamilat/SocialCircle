import { Component } from '@angular/core';
import { RouterOutlet,  ActivationEnd } from '@angular/router';
import { RegisterComponent } from './features/accounts/register/register.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    RegisterComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Social app 2';
}
