import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

import { DxDataGridComponent } from 'devextreme-angular/ui/data-grid';
import notify from 'devextreme/ui/notify';
import { confirm } from 'devextreme/ui/dialog';

import { GlobalConstants } from '../../common/globalConstants';
import { EquipmentModel } from '../equipmentModel';
import { ApiResult } from '../../common/apiResult';

@Component({
  selector: 'app-equipment-list',
  templateUrl: './equipment-list.component.html',
  styleUrls: ['./equipment-list.component.css']
})
export class EquipmentListComponent implements OnInit {

  equipmentList: any = [];

  constructor(private httpClient: HttpClient, private router: Router) { }

  ngOnInit() {

    this.getEquipments();
  }

  /**
  * Grid toolbar loaded
  * @param event :any
  */
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

  /**
 * redirects to view detail of selected equipment item
 */
  onViewClicked = (event: any) => {

    this.navigateToStep(event, 'equipment-detail', 'view');
  }

  /**
   * Determines whether edit clicked on
   */
  onEditClicked = (event: any) => {

    this.navigateToStep(event, 'equipment-detail', 'edit');

  }

  /**
   * Delete event is clicked
   */
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

  /**
  * Delete event is clicked
  */
  onAddClicked = (event: any) => {
    this.navigateToStep(event, 'clinic-detail', 'add');
  }

  private deleteRecord(id: number) {

    if (!id) {
      return;
    }

    const endpoint = 'equipments/' + id;

    this.httpClient.delete(GlobalConstants.apiURL + endpoint).subscribe(result => {

      const apiResult: ApiResult<boolean> = result as ApiResult<boolean>;

      if (apiResult.isSucceed) {
        this.getEquipments();
        return;
      }

      notify(apiResult.errorMessage, 'error');

    }, error => notify(error)
    );

  }

  private navigateToStep(event: any, routeName: string, viewType: string) {

    let param = null;
    if (event && event.row && event.row.data && event.row.data.id) {
      param = event.row.data.id;
    }

    this.router.navigate(['equipment-detail'], {
      queryParams:
      {
        id: param,
        viewType: viewType
      }
    });

  }

  private getEquipments() {

    this.httpClient.get(GlobalConstants.apiURL + 'equipments').subscribe(result => {

      const apiResult: ApiResult<EquipmentModel> = result as ApiResult<EquipmentModel>;

      if (apiResult.isSucceed) {
        this.equipmentList = apiResult.result;
      }

    }, error => notify(error.message));

  }

}
