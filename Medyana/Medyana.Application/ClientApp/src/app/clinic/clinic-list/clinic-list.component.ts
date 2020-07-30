import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

import notify from 'devextreme/ui/notify';
import { confirm } from 'devextreme/ui/dialog';

import { GlobalConstants } from '../../common/globalConstants';
import { ClinicModel } from '../clinicModel';
import { ApiResult } from '../../common/apiResult';

@Component({
  selector: 'app-clinic-list',
  templateUrl: './clinic-list.component.html',
  styleUrls: ['./clinic-list.component.css']
})
export class ClinicListComponent implements OnInit {

  clinicList: any = [];

  constructor(private httpClient: HttpClient, private router: Router) { }

  ngOnInit() {
    this.getClinics();
  }

  onToolbarPreparing(event) {
    event.toolbarOptions.items.unshift({
      location: 'before',
      widget: 'dxButton',
      options: {
        icon: 'add',
        onClick: this.onAddClicked.bind(this)
      }
    });
  }

  onViewClicked = (event: any) => {

    this.navigateToStep(event, 'view');
  }

  onEditClicked = (event: any) => {

    this.navigateToStep(event, 'edit');

  }

  onDeleteClicked = (event: any) => {

    const result = confirm(
      '<i>Are you sure you want to delete this record?</i>', 'Confirm changes'
    );

    result.then(dialogResult => {
      if (dialogResult) {
        this.deleteRecord(event.row.data.id);
      }
    });

  }

  onAddClicked = (event: any) => {
    this.navigateToStep(event, 'add');
  }

  private getClinics() {

    this.httpClient.get(GlobalConstants.apiURL + 'clinics').subscribe(result => {

      const apiResult: ApiResult<ClinicModel> = result as ApiResult<ClinicModel>;

      if (apiResult.isSucceed) {
        this.clinicList = apiResult.result;
      }

    }, error => notify(error.message));

  }

  private deleteRecord(id: number) {

    if (!id) {
      return;
    }

    const endpoint = 'clinics/' + id;

    this.httpClient.delete(GlobalConstants.apiURL + endpoint).subscribe(result => {

      const apiResult: ApiResult<boolean> = result as ApiResult<boolean>;

      if (apiResult.isSucceed) {
        this.getClinics();
        return;
      }

      notify(apiResult.errorMessage, 'error');

    }, error => notify(error.message)
    );

  }

  private navigateToStep(event: any, viewType: string) {

    let param = null;
    if (event && event.row && event.row.data && event.row.data.id) {
      param = event.row.data.id;
    }
    this.router.navigate(['clinic-detail'], {
      queryParams:
      {
        id: param,
        viewType: viewType
      }
    });

  }

}
