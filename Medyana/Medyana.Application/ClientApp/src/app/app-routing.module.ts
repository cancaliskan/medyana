import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClinicsComponent } from './clinics/clinics.component';
import { ClinicComponent } from './clinic/clinic.component';
import { ClinicAddEditComponent } from './clinic-add-edit/clinic-add-edit.component';

const routes: Routes = [
  { path: '', component: ClinicsComponent, pathMatch: 'full' },
  { path: 'clinic/:id', component: ClinicComponent },
  { path: 'add', component: ClinicAddEditComponent },
  { path: 'clinic/edit/:id', component: ClinicAddEditComponent },
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
