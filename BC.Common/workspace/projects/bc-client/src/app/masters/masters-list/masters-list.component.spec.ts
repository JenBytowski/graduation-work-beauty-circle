import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MastersListComponent } from './masters-list.component';

describe('MastersListComponent', () => {
  let component: MastersListComponent;
  let fixture: ComponentFixture<MastersListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MastersListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MastersListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
