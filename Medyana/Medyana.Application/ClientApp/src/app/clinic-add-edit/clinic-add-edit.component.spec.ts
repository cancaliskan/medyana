import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClinicAddEditComponent } from './clinic-add-edit.component';

describe('ClinicAddEditComponent', () => {
  let component: ClinicAddEditComponent;
  let fixture: ComponentFixture<ClinicAddEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClinicAddEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClinicAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
