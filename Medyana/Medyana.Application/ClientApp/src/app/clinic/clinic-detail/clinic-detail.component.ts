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

import { ClinicModel } from '../clinicModel';
import { ApiResult } from '../../common/apiResult';
import { GlobalConstants } from 'src/app/common/globalConstants';

@Component({
  selector: 'app-clinic-detail',
  templateUrl: './clinic-detail.component.html',
  styleUrls: ['./clinic-detail.component.css']
})
export class ClinicDetailComponent implements OnInit {

  @ViewChild(DxFormComponent, { static: false }) myform: DxFormComponent;

  sub: any;
  selectedRecordId: number;
  viewType: string;
  clinicRecord: ClinicModel;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private httpClient: HttpClient,
    private location: Location
  ) {

  }

  ngOnInit() {
    this.sub = this.route
      .queryParams
      .subscribe(params => {
        this.selectedRecordId = params['id'] || 0;
        this.viewType = params['viewType'] || '';

        if (this.viewType !== 'add') {
          this.getClinicDetailById();
        } else {
          this.clinicRecord = new ClinicModel();
        }

      });
  }

  onCloseClick = () => {
    this.location.back();
  }

  isDisabled() {
    return (this.viewType === 'view');
  }

  getViewType() {
    return this.viewType;
  }

  onSaveClick = (event: any) => {

    event.preventDefault();
    if (this.myform.instance.validate().isValid === false) {
      return;
    }

    if (this.viewType === 'edit') {
      this.updateClinic();
    } else if (this.viewType === 'add') {
      this.addClinic();
    }

  }

  private getClinicDetailById() {

    const endpoint = 'clinics/' + this.selectedRecordId;

    this.httpClient.get(GlobalConstants.apiURL + endpoint).subscribe(result => {

      const apiResult: ApiResult<ClinicModel> = result as ApiResult<ClinicModel>;

      if (apiResult.isSucceed) {

        this.clinicRecord = apiResult.result;
        return;

      }

      notify(apiResult.errorMessage, 'error');

    }, error => notify(error.message));

  }

  private updateClinic() {

    const endpoint = 'clinics';

    this.httpClient.put(GlobalConstants.apiURL + endpoint, this.clinicRecord).subscribe(result => {

      const apiResult: ApiResult<ClinicModel> = result as ApiResult<ClinicModel>;

      if (apiResult.isSucceed) {

        this.clinicRecord = apiResult.result;
        this.router.navigate(['/clinic-list']);
        return;
      }

      notify(apiResult.errorMessage, 'error');

    }, error => notify(error.message));

  }

  private addClinic() {

    const endpoint = 'clinics';

    const headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });

    this.httpClient.post(GlobalConstants.apiURL + endpoint, this.clinicRecord, {
      headers: headers
    }).subscribe(result => {

      const apiResult: ApiResult<ClinicModel> = result as ApiResult<ClinicModel>;

      if (apiResult.isSucceed) {

        this.clinicRecord = apiResult.result;
        this.router.navigate(['/clinic-list']);
        return;
      }

      notify(apiResult.errorMessage, 'error');

    }, error => notify(error.message));

  }
}
