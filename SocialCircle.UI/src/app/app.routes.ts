import { Routes } from '@angular/router';
import { HomeComponent } from './features/users/home/home.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { RegisterComponent } from './features/accounts/register/register.component';
import { LoginComponent } from './features/accounts/login/login.component';
import { AuthGuard } from './core/gaurds/auth-guard';
import { MainComponent } from './shared/layouts/main/main.component';

export const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'home'
    },
    {
        canActivate: [AuthGuard],
        path: 'home',
        component: MainComponent,
        children: [
            {
                path: '',
                pathMatch: 'full',
                component: HomeComponent,
                data: {
                    title: 'User home page'
                }
            }
        ]
    },
    { path: 'register', component: RegisterComponent },
    { path: 'login', component: LoginComponent },
    {
        path: '**',
        component: NotFoundComponent
    },
];