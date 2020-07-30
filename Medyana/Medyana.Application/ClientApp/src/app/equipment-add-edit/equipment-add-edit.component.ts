import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { EquipmentService } from '../services/equipment.service';
import { Equipment, Result } from '../models/equipment';

@Component({
  selector: 'app-equipment-add-edit',
  templateUrl: './equipment-add-edit.component.html',
  styleUrls: ['./equipment-add-edit.component.scss']
})
export class EquipmentAddEditComponent implements OnInit {
  form: FormGroup;
  actionType: string;
  id: number;
  name: string;
  formName: string;
  // formProvideDate?: any;
  // formQuantity: number;
  // formUnitPrice: number;
  // formUsageRate: number;
  // formClinicId: number;
  errorMessage: any;
  existingEquipment: Result;

  // tslint:disable-next-line: max-line-length
  constructor(private equipmentService: EquipmentService, private formBuilder: FormBuilder, private avRoute: ActivatedRoute, private router: Router) {
    const idParam = 'id';
    this.actionType = 'Add';
    this.formName = 'name';
    // this.formProvideDate = 0;
    // this.formQuantity = 0;
    // this.formUnitPrice = 0;
    // this.formUsageRate = 0;
    // this.formClinicId = 0;

    if (this.avRoute.snapshot.params[idParam]) {
      this.id = this.avRoute.snapshot.params[idParam];
    }

    this.form = this.formBuilder.group(
      {
        id: 0,
        name: ['', [Validators.required]],
        quantity: ['', [Validators.min(1)]],
        unitPrice: ['', [Validators.min(0.01)]],
      }
    );
  }

  ngOnInit() {
    if (this.id > 0) {
      this.actionType = 'Edit';
      this.equipmentService.getEquipment(this.id).subscribe(
        (res: any) => {
          this.equipmentService = res.result;
          this.form.controls[this.formName].setValue(res.result.name);
        },
      );
    }
  }

  cancel() {
    this.router.navigate(['/equipments']);
  }

  // get name() { return this.form.get(this.formName); }
  // get quantity() { return this.form.get(this.formQuantity); }
  // get unitPrice() { return this.form.get(this.formUnitPrice); }
}
