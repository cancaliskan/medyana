import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ClinicService } from '../services/clinic.service';
import { Clinic } from '../models/clinic';

@Component({
  selector: 'app-clinic-add-edit',
  templateUrl: './clinic-add-edit.component.html',
  styleUrls: ['./clinic-add-edit.component.scss']
})
export class ClinicAddEditComponent implements OnInit {
  form: FormGroup;
  actionType: string;
  name: string;
  id: number;
  errorMessage: any;
  existingClinic: Clinic;

  // tslint:disable-next-line: max-line-length
  constructor(private clinicService: ClinicService, private formBuilder: FormBuilder, private avRoute: ActivatedRoute, private router: Router) {
    const idParam = 'id';
    this.actionType = 'Add';
    this.name = 'name';
    if (this.avRoute.snapshot.params[idParam]) {
      this.id = this.avRoute.snapshot.params[idParam];
    }

    this.form = this.formBuilder.group(
      {
        id: 0,
        name: ['', [Validators.required]],
      }
    );
  }

  ngOnInit() {
    if (this.id > 0) {
      this.actionType = 'Edit';
      this.clinicService.getClinic(this.id).subscribe(
        (res: any) => {
          this.existingClinic = res.result;
          this.form.controls[this.name].setValue(res.result.name);
        },
      );
    }
  }

  save() {
    if (!this.form.valid) {
      return;
    }

    if (this.actionType === 'Add') {
      const clinic: any = {
        name: this.form.get(this.name).value,
      };

      this.clinicService.saveClinic(clinic)
        .subscribe((data) => {
          this.router.navigate(['/clinics', data.result.id]);
        });
    }

    if (this.actionType === 'Edit') {
      const clinic: any = {
        id: this.existingClinic.id,
        name: this.form.get(this.name).value
      };

      this.clinicService.updateClinic(clinic)
        .subscribe((data) => {
          this.router.navigate(['/clinics']);
        });
    }
  }

  cancel() {
    this.router.navigate(['/clinics']);
  }

  get getName() { return this.form.get(this.name); }
}
