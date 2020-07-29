import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ClinicService } from '../services/clinic.service';
import { Clinic } from '../models/clinic';

@Component({
  selector: 'app-clinics',
  templateUrl: './clinics.component.html',
  styleUrls: ['./clinics.component.scss']
})
export class ClinicsComponent implements OnInit {
  clinics: Observable<Clinic>;

  constructor(private clinicService: ClinicService) { }

  ngOnInit() {
    this.loadClinics();
  }

  loadClinics() {
    this.clinicService.getClinics().subscribe(
      (res: any) => {
        this.clinics = res.result;
      },
    );
  }

  delete(id) {
    const ans = confirm('Do you want to delete clinic with id: ' + id);
    if (ans) {
      this.clinicService.deleteClinic(id).subscribe((data) => {
        this.loadClinics();
      });
    }
  }
}
