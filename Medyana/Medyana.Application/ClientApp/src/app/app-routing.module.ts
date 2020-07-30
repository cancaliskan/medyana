import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ClinicsComponent } from './clinics/clinics.component';
import { ClinicComponent } from './clinic/clinic.component';
import { ClinicAddEditComponent } from './clinic-add-edit/clinic-add-edit.component';

import { EquipmentsComponent } from './equipments/equipments.component';
import { EquipmentComponent } from './equipment/equipment.component';
import { EquipmentAddEditComponent } from './equipment-add-edit/equipment-add-edit.component';

const routes: Routes = [
  { path: 'clinics', component: ClinicsComponent, pathMatch: 'full' },
  { path: 'clinics/add', component: ClinicAddEditComponent },
  { path: 'clinics/:id', component: ClinicComponent },
  { path: 'clinics/edit/:id', component: ClinicAddEditComponent },

  { path: 'equipments', component: EquipmentsComponent, pathMatch: 'full' },
  { path: 'equipments/add', component: EquipmentAddEditComponent },
  { path: 'equipments/:id', component: EquipmentComponent },
  { path: 'equipments/edit/:id', component: EquipmentAddEditComponent },

  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
