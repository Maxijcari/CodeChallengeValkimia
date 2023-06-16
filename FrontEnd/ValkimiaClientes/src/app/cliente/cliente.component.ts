import { ClienteService } from '../services/cliente.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Cliente } from '../models/cliente';
@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnInit {
  
  clienteForm: FormGroup;
  clientes:Cliente[]=[];
  clientId:string="";
  ciudades: { Id: string, Ciudad: string }[] = [
    { Id: '00000000-0000-0000-0000-000000000001', Ciudad: 'Ciudad A' },
    { Id: '00000000-0000-0000-0000-000000000002', Ciudad: 'Ciudad B' },
    { Id: '00000000-0000-0000-0000-000000000003', Ciudad: 'Ciudad C' }
  ];

  constructor(private clienteService:ClienteService) {
    this.clienteForm = new FormGroup({
      Nombre: new FormControl('', [Validators.required, Validators.maxLength(50)]),
      Apellido: new FormControl('', [Validators.required, Validators.maxLength(50)]),
      Domicilio: new FormControl('', [Validators.required, Validators.maxLength(50)]),
      Email: new FormControl('', [Validators.required, Validators.email, Validators.maxLength(100)]),
      Password: new FormControl('', [Validators.required, Validators.maxLength(100)]),
      Habilitado: new FormControl(false),
      CiudadId: new FormControl('', Validators.required),
    });
    
  }

  ngOnInit(): void {
    this.clientId="";
    this.GetClientes();
  }

  GetClientes(){
    this.clienteService.GetClientes().subscribe(
      (response) => {
        // Lógica para el manejo de la respuesta exitosa
        console.log('clientes traidos', response);
        this.clientes=JSON.parse(response.toString());
        this.clientes.forEach(cliente => {
          let ciudad = this.ciudades.find((c) => c.Id === cliente.CiudadId);
          cliente.Ciudad = ciudad ? ciudad.Ciudad : "";
        });
      },
      (error) => {
        // Lógica para el manejo de errores
        console.error('error al cargar clientes', error);
      }
    );
  }
  //Metodos para el ABM
  GuardarCliente() {
    if (this.clienteForm.valid) {
      let cliente:Cliente = this.clienteForm.value;
      cliente.Habilitado = this.clienteForm.get('Habilitado')?.value=="true"?true:false;
      if(this.clientId)
      {//Editamos el cliente
        this.EditarCliente(this.clientId,cliente)
      }else{
        //Creamos el cliente
        this.CrearCliente(cliente);
      }
      
    } else {
      console.log("El formulario no es válido");
    }
    this.clienteForm.reset();
    this.ngOnInit();
  }

  EditarCliente(Id:string,cliente:Cliente){
    this.clienteService.EditarCliente(Id,cliente).subscribe(
      (response) => {
        // Lógica para el manejo de la respuesta exitosa
        console.log('Cliente Editado con éxito', response);
      },
      (error) => {
        // Lógica para el manejo de errores
        console.error('Error al editar el cliente', error);
      }
    );
  }

  CrearCliente(cliente:Cliente){
    this.clienteService.CrearCliente(cliente).subscribe(
      (response) => {
        // Lógica para el manejo de la respuesta exitosa
        console.log('Cliente guardado con éxito', response);
      },
      (error) => {
        // Lógica para el manejo de errores
        console.error('Error al guardar el cliente', error);
      }
    );
  }


  CargarClienteAForm(index:number,clienteAModificar:Cliente){
    this.clientId=this.clientes[index].Id;
    this.clienteForm.patchValue({
      Nombre: clienteAModificar.Nombre,
      Apellido: clienteAModificar.Apellido,
      Domicilio: clienteAModificar.Domicilio,
      Email: clienteAModificar.Email,
      Password: clienteAModificar.Password,
      Habilitado: clienteAModificar.Habilitado,
      CiudadId: clienteAModificar.CiudadId
    });
    
  }

  EliminarCliente(index:number){
    this.clientId=this.clientes[index].Id;
    this.clienteService.EliminarCliente(this.clientId).subscribe(
      (response) => {
        // Lógica para el manejo de la respuesta exitosa
        console.log('Cliente Borrado', response);
        this.ngOnInit();
      },
      (error) => {
        // Lógica para el manejo de errores
        console.error('No se pudo borrar', error);
      }
    );
  }

}
