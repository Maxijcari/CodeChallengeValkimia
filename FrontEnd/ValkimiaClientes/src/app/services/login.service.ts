import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private apiUrl = 'http://localhost:5206/api';
  constructor(private http: HttpClient) {}

  login(Email:string,Password:string){
    const credenciales = {Email,Password};
    return this.http.post(`${this.apiUrl}/Login`, credenciales);
  }
}
