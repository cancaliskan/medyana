import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentAddEditComponent } from './equipment-add-edit.component';

describe('EquipmentAddEditComponent', () => {
  let component: EquipmentAddEditComponent;
  let fixture: ComponentFixture<EquipmentAddEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EquipmentAddEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EquipmentAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
