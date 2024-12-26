import { Routes } from '@angular/router';
import { HomeComponent } from './features/users/home/home.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { RegisterComponent } from './features/accounts/register/register.component';
import { LoginComponent } from './features/accounts/login/login.component';

export const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'home'
    },
    {
        path: 'home',
        component: HomeComponent
    },
    { path: 'register', component: RegisterComponent },
    { path: 'login', component: LoginComponent },
    {
        path: '**',
        component: NotFoundComponent
    },
];