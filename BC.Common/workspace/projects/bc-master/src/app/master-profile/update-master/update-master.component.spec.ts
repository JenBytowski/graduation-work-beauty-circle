import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateMasterComponent } from './update-master.component';

describe('UpdateMasterComponent', () => {
  let component: UpdateMasterComponent;
  let fixture: ComponentFixture<UpdateMasterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateMasterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateMasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
