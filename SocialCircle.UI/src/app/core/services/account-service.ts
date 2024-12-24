import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class AccountService {
    constructor(private http: HttpClient) { }
    url = 'http://localhost:5550/api/Accounts/Register'

    // Zohaib, create response model, remove any
    // TODO: Huzaifa, create interface, remove any
    register(registerModel: any): Observable<any> {
        return this.http.post<any>(this.url, registerModel)
    }
} 