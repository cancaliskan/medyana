import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { ClinicService } from '../services/clinic.service';
import { Clinic } from '../models/clinic';

@Component({
  selector: 'app-clinic',
  templateUrl: './clinic.component.html',
  styleUrls: ['./clinic.component.scss']
})
export class ClinicComponent implements OnInit {
  clinic$: Observable<Clinic>;
  postId: number;

  constructor(private clinicService: ClinicService, private avRoute: ActivatedRoute) {
    const idParam = 'id';
    if (this.avRoute.snapshot.params[idParam]) {
      this.postId = this.avRoute.snapshot.params[idParam];
    }
  }

  ngOnInit() {
    this.loadClinic();
  }

  loadClinic() {
    this.clinicService.getClinic(this.postId).subscribe(
      (res: any) => {
        this.clinic$ = res.result;
      },
    );
  }
}
