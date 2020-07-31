import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient, HttpInterceptor, HttpHandler } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';

import { ClinicModule } from './clinic/clinic.module';
import { ClinicListComponent } from './clinic/clinic-list/clinic-list.component';


import { EquipmentModule } from './equipment/equipment.module';
import { EquipmentListComponent  } from './equipment/equipment-list/equipment-list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ClinicModule,
    EquipmentModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'clinic-list', component: ClinicListComponent },
      { path: 'equipment-list', component: EquipmentListComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
