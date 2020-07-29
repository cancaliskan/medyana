import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ClinicsComponent } from './clinics/clinics.component';
import { ClinicComponent } from './clinic/clinic.component';
import { ClinicAddEditComponent } from './clinic-add-edit/clinic-add-edit.component';
import { ClinicService } from './services/clinic.service';

@NgModule({
  declarations: [
    AppComponent,
    ClinicsComponent,
    ClinicComponent,
    ClinicAddEditComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule
  ],
  providers: [
    ClinicService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
