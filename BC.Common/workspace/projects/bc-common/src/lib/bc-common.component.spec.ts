import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {BcCommonComponent} from './bc-common.component';

describe('BcCommonComponent', () => {
  let component: BcCommonComponent;
  let fixture: ComponentFixture<BcCommonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [BcCommonComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BcCommonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
