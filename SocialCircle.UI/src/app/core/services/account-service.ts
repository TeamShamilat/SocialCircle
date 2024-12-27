import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { LoginModel, LoginResponseModel, RegisterModel } from "../../Interface/accounts-models";
import { API } from "../../shared/constants/api-contstants";

@Injectable({
    providedIn: 'root'
})
export class AccountService {
    constructor(private http: HttpClient) { }
    // TODO: replace any with actual
    register(registerModel: RegisterModel): Observable<any> {
        return this.http.post<any>(`${API.baseUrl}/${API.accountEndPoints.register}`, registerModel)
    }

    login(model: LoginModel): Observable<LoginResponseModel> {
        return this.http.post<LoginResponseModel>(`${API.baseUrl}/${API.accountEndPoints.login}`, model)
    }
} 