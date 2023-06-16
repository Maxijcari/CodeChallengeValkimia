import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { HttpClientModule} from '@angular/common/http';
import { ClienteComponent } from './cliente/cliente.component';
import { FacturaComponent } from './factura/factura.component';
import { DatePipe } from '@angular/common';
import { PrincipalComponent } from './principal/principal.component';
import { RouterModule  } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ClienteComponent,
    FacturaComponent,
    PrincipalComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule,
    AppRoutingModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
