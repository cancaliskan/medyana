import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { EquipmentService } from '../services/equipment.service';
import { Equipment } from '../models/equipment';

@Component({
  selector: 'app-equipment',
  templateUrl: './equipment.component.html',
  styleUrls: ['./equipment.component.scss']
})
export class EquipmentComponent implements OnInit {
  equipment$: Observable<Equipment>;
  postId: number;

  constructor(private equipmentService: EquipmentService, private avRoute: ActivatedRoute) {
    const idParam = 'id';
    if (this.avRoute.snapshot.params[idParam]) {
      this.postId = this.avRoute.snapshot.params[idParam];
    }
  }

  ngOnInit() {
    this.loadEquipment();
  }

  loadEquipment() {
    this.equipmentService.getEquipment(this.postId).subscribe(
      (res: any) => {
        this.equipment$ = res.result;
      },
    );
  }

}
