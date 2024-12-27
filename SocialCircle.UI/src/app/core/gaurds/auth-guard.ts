import { Injectable } from "@angular/core";
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivateFn } from "@angular/router";

@Injectable({
    providedIn: 'root'
})

@Injectable({
    providedIn: 'root',
})
export class AuthGuard implements CanActivate {
    constructor(private router: Router) { }

    canActivate(): boolean {
        if (this.isLoggedIn) {
            return true; // Allow access
        } else {
            this.router.navigate(['/login']);
            return false; // Deny access
        }
    }

    get isLoggedIn(): boolean {
        return !!localStorage.getItem('access_token');
    }
}
