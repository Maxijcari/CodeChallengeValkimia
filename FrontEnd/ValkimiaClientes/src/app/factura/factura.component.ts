import { Component, OnInit } from '@angular/core';
import { Factura } from '../models/factura';
import { FacturaService } from '../services/factura.service';
import { FormGroup,Validators,FormControl } from '@angular/forms';
import { ClienteService } from '../services/cliente.service';
import { Cliente } from '../models/cliente';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-factura',
  templateUrl: './factura.component.html',
  styleUrls: ['./factura.component.css']
})
export class FacturaComponent implements OnInit {

  facturaForm:FormGroup;
  facturaId:string='';
  facturas:Factura[]=[];
  clientes:Cliente[]=[];
  fechaString: string='';
  constructor(private facturaService:FacturaService, private clienteService:ClienteService,private datePipe:DatePipe) 
  {
    this.facturaForm = new FormGroup({
      ClienteId: new FormControl('', [Validators.required, Validators.maxLength(50)]),
      Fecha: new FormControl('', [Validators.required]),
      Importe: new FormControl('', [Validators.required, Validators.maxLength(50)]),
      Detalle: new FormControl('', [Validators.required, Validators.maxLength(200)]),
    });
  }

  ngOnInit(): void {
    this.facturaId='';
    this.GetClientes();
    this.GetFacturas();
  }

  GetClientes(){
    this.clienteService.GetClientes().subscribe(
      (response)=>{
        this.clientes=JSON.parse(response.toString());
      },
      (error)=>{

      }
    )
  }

  GetFacturas(){
    this.facturaService.GetFacturas().subscribe(
      (respose)=>{
        this.facturas=JSON.parse(respose.toString());
        this.facturas.forEach(factura => {
          let cliente = this.clientes.find((c) => c.Id === factura.ClienteId);
          factura.Cliente = cliente ? cliente?.Nombre +" "+ cliente?.Apellido : "";
          // const fechaTexto: string = factura.Fecha.toString();
          // const fechaSplit: string[] = fechaTexto.split('T')[0].split('-');
          // const fechaFormateada: string = `${fechaSplit[2]}-${fechaSplit[1]}-${fechaSplit[0]}`;
        });
      },
      (error)=>{

      }
    )
  }

  GuardarFactura(){
    if (this.facturaForm.valid) {
      let factura:Factura = this.facturaForm.value;
      if(this.facturaId)
      {//Editamos el cliente
        this.EditarFactura(this.facturaId,factura)
      }else{
        //Creamos el cliente
        this.CrearFactura(factura);
      }
      
    } else {
      console.log("El formulario no es válido");
    }
    this.facturaForm.reset();
    this.ngOnInit();
  }

  CrearFactura(factura:Factura){
    this.facturaService.CrearFactura(factura).subscribe(
      (response)=>{
        console.log('Factura guardada con éxito', response);
        this.ngOnInit();
      },
      (error)=>{
        console.error('Error al guardar Factura', error);
      }
    );
  }

  // formatoFecha(fecha: string): string {
  //   const fechaObj = new Date(fecha);
    
  //   if(fechaObj){
  //     return this.datePipe.transform(fechaObj, 'MM/dd/yyyy');
  //   }
  //     return '';
  // }

  EditarFactura(Id:string,factura:Factura){
    this.facturaService.EditarFactura(Id,factura).subscribe(
      (response)=>{
        // Lógica para el manejo de la respuesta exitosa
        console.log('Factura Editada con éxito', response);
      },
      (error)=>{
        // Lógica para el manejo de errores
        console.error('Error al editar la factura', error);
      }
    )
  }

  CargarFacturaAForm(index:number,facturaAModificar:Factura){
    this.facturaId=this.facturas[index].Id;
    let fecha = new Date(facturaAModificar.Fecha.toString());
    this.facturaForm.patchValue({
      ClienteId: facturaAModificar.ClienteId,
      Fecha: this.datePipe.transform(fecha, 'yyyy-MM-dd') ?? '',
      Importe: facturaAModificar.Importe,
      Detalle: facturaAModificar.Detalle
    });
  }
  
  EliminarFactura(index:number){
    this.facturaService.EliminarFactura(this.facturas[index].Id).subscribe(
      (response) => {
        // Lógica para el manejo de la respuesta exitosa
        console.log('Factura Borrada', response);
        this.ngOnInit();
      },
      (error) => {
        // Lógica para el manejo de errores
        console.error('No se pudo borrar factura', error);
      }
    )
  }
}
