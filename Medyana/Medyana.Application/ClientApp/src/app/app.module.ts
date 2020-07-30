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

import { EquipmentsComponent } from './equipments/equipments.component';
import { EquipmentComponent } from './equipment/equipment.component';
import { EquipmentAddEditComponent } from './equipment-add-edit/equipment-add-edit.component';
import { EquipmentService } from './services/equipment.service';

@NgModule({
  declarations: [
    AppComponent,

    ClinicsComponent,
    ClinicComponent,
    ClinicAddEditComponent,

    EquipmentsComponent,
    EquipmentComponent,
    EquipmentAddEditComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule
  ],
  providers: [
    ClinicService,
    EquipmentService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
