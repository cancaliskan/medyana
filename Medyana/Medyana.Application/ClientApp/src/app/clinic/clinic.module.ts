import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { ClinicListComponent } from './clinic-list/clinic-list.component';
import { ClinicDetailComponent } from './clinic-detail/clinic-detail.component';

import {
  DxListModule,
  DxDataGridModule,
  DxFormModule
} from 'devextreme-angular';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    DxDataGridModule,
    DxFormModule,
    RouterModule.forChild([
      {
        path: 'clinic-list',
        component: ClinicListComponent
      },
      {
        path: 'clinic-detail',
        component: ClinicDetailComponent
      }
    ])
  ],
  declarations: [
    ClinicListComponent,
    ClinicDetailComponent
  ]
})
export class ClinicModule {
  constructor() {
  }
}
