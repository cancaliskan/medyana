import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Location } from '@angular/common';

import notify from 'devextreme/ui/notify';
import { confirm } from 'devextreme/ui/dialog';
import {
  DxCheckBoxModule,
  DxSelectBoxModule,
  DxNumberBoxModule,
  DxFormModule,
  DxFormComponent
} from 'devextreme-angular';

import { EquipmentModel } from '../equipmentModel';
import { ApiResult } from '../../common/apiResult';
import { GlobalConstants } from 'src/app/common/globalConstants';
import { ClinicModel } from '../../clinic/clinicModel';

@Component({
  selector: 'app-equipment-detail',
  templateUrl: './equipment-detail.component.html',
  styleUrls: ['./equipment-detail.component.css']
})
export class EquipmentDetailComponent implements OnInit {

  @ViewChild(DxFormComponent, { static: false }) myform: DxFormComponent;

  sub: any;
  selectedRecordId: number;
  viewType: string;
  equipmentRecord: EquipmentModel;
  clinicList: ClinicModel[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private httpClient: HttpClient,
    private location: Location
  ) { }

  ngOnInit() {

    this.fillClinics();

    this.sub = this.route
      .queryParams
      .subscribe(params => {
        this.selectedRecordId = params['id'] || 0;
        this.viewType = params['viewType'] || '';
        
        if (this.viewType !== 'add') {
          this.getEquipmentDetailById();
        } else {
          this.equipmentRecord = new EquipmentModel();
        }
      });
  }

  onCloseClick = () => {
    this.location.back();
  }

  isDisabled() {
    return (this.viewType === 'view');
  }

  onSaveClick = () => {

    if (this.myform.instance.validate().isValid === false) {
      return;
    }

    if (this.viewType === 'edit') {
      this.updateEquipment();
    }else if (this.viewType === 'add') {
      this.addEquipment();
    }
  }

  private getEquipmentDetailById() {

    const endpoint = 'equipments/' + this.selectedRecordId;

    this.httpClient.get(GlobalConstants.apiURL + endpoint).subscribe(result => {

      const apiResult: ApiResult<EquipmentModel> = result as ApiResult<EquipmentModel>;

      if (apiResult.isSucceed) {

        this.equipmentRecord = apiResult.result;
        return;

      }

      notify(apiResult.errorMessage, 'error');

    }, error => notify(error.message));

  }

  private updateEquipment() {

    const endpoint = 'equipments';

    this.httpClient.put(GlobalConstants.apiURL + endpoint, this.equipmentRecord).subscribe(result => {

      const apiResult: ApiResult<EquipmentModel> = result as ApiResult<EquipmentModel>;

      if (apiResult.isSucceed) {

        this.equipmentRecord = apiResult.result;
        this.router.navigate(['/equipment-list']);
        return;
      }

      notify(apiResult.errorMessage, 'error');

    }, error => notify(error.message));

  }

  private fillClinics() {

    this.httpClient.get(GlobalConstants.apiURL + 'clinics').subscribe(result => {

      const apiResult: ApiResult<ClinicModel[]> = result as ApiResult<ClinicModel[]>;

      if (apiResult.isSucceed) {

        this.clinicList = apiResult.result;
      }

    }, error => notify(error.message));

  }

  private addEquipment() {

    const endpoint = 'equipments';
    const headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });

    this.httpClient.post(GlobalConstants.apiURL + endpoint, this.equipmentRecord, {
      headers: headers
    }).subscribe(result => {

      const apiResult: ApiResult<EquipmentModel> = result as ApiResult<EquipmentModel>;

      if (apiResult.isSucceed) {

        this.equipmentRecord = apiResult.result;
        this.router.navigate(['/equipment-list']);
        return;
      }

      notify(apiResult.errorMessage, 'error');

    }, error => notify(error.message));
  }

}
