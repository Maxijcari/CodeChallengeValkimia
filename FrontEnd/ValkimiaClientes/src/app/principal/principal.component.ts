import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-principal',
  templateUrl: './principal.component.html',
  styleUrls: ['./principal.component.css']
})
export class PrincipalComponent implements OnInit {

  opcionSeleccionada: string = 'cliente'; // Valor inicial: cliente
  constructor(private router:Router) { }

  ngOnInit(): void {
  }
  redirigirCliente() {
    this.opcionSeleccionada = 'cliente';
  }

  redirigirFactura() {
    this.opcionSeleccionada = 'factura';
  }

  cerrarSesion(){
    this.router.navigateByUrl('/login');
  }

}
