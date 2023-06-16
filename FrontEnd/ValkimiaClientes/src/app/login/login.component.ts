import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
email: string="";
password: string="";

  constructor(private loginService:LoginService,private router:Router) { }

  ngOnInit(): void {
  }
  public login(){
    this.loginService.login(this.email,this.password).subscribe(
      (response) => {
        // Lógica para el manejo de la respuesta exitosa
        this.router.navigateByUrl('/principal');
        console.log('Inicio de sesión exitoso', response);
      },
      (error) => {
        if (error instanceof HttpErrorResponse) {
          console.error('Error al iniciar sesión:', error.status, error.statusText);
        } else {
          console.error('Error al iniciar sesión:', error);
        }
      }
    );
  }
}
