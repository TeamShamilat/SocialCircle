export interface RegisterModel {
    name: string;
    email: string;
    password: string;
}

export interface LoginModel { 
   userName: string;
   password: string;
  }
  
  export interface LoginResponseModel {
  token: string;
  refreshToken: string;
}
