import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Cliente } from '../models/cliente';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  
  private apiUrl = 'http://localhost:5206/api/Cliente';
  constructor(private http:HttpClient) { }
  
  GetClientes(){
    return this.http.get(`${this.apiUrl}`)
  }
  CrearCliente(cliente:Cliente){
    return this.http.post(`${this.apiUrl}`, cliente);
  }
  EditarCliente(Id:string,cliente:Cliente){
    return this.http.put(`${this.apiUrl}/`+Id,cliente)
  }
  EliminarCliente(Id:string){
    return this.http.delete(`${this.apiUrl}/`+Id)
  }
}
