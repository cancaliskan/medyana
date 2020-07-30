import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { EquipmentService } from '../services/equipment.service';
import { Equipment} from '../models/equipment';

@Component({
  selector: 'app-equipments',
  templateUrl: './equipments.component.html',
  styleUrls: ['./equipments.component.scss']
})
export class EquipmentsComponent implements OnInit {
  equipments$: Observable<Equipment>;

  constructor(private equipmentService: EquipmentService) { }

  ngOnInit() {
    this.loadEquipments();
  }

  loadEquipments() {
    this.equipmentService.getEquipments().subscribe(
      (res: any) => {
        this.equipments$ = res.result;
      },
    );
  }

  delete(id) {
    const ans = confirm('Do you want to delete equipment with id: ' + id);
    if (ans) {
      this.equipmentService.deleteEquipment(id).subscribe((data) => {
        this.loadEquipments();
      });
    }
  }
}
