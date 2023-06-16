import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Factura } from '../models/factura';

@Injectable({
  providedIn: 'root'
})
export class FacturaService {

  private apiUrl = 'http://localhost:5206/api/Factura';
  constructor(private http:HttpClient) { }

  GetFacturas(){
    return this.http.get(`${this.apiUrl}`);
  }
  CrearFactura(factura:Factura){
    return this.http.post(`${this.apiUrl}`,factura);
  }
  EditarFactura(Id:string,factura:Factura){
    return this.http.put(`${this.apiUrl}/`+Id,factura);
  }
  EliminarFactura(Id:string){
    return this.http.delete(`${this.apiUrl}/`+Id);
  }
}
