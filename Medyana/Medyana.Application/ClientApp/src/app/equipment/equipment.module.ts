import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { EquipmentListComponent } from './equipment-list/equipment-list.component';
import { EquipmentDetailComponent } from './equipment-detail/equipment-detail.component';

import {
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
        path: 'equipment-list',
        component: EquipmentListComponent
      },
      {
        path: 'equipment-detail',
        component: EquipmentDetailComponent
      }
    ])
  ],
  declarations: [
    EquipmentListComponent,
    EquipmentDetailComponent
  ]
})
export class EquipmentModule {
  constructor() {
  }
}
