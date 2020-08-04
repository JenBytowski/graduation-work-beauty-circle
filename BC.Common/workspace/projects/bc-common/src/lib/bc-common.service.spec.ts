import { TestBed } from '@angular/core/testing';

import { BcCommonService } from './bc-common.service';

describe('BcCommonService', () => {
  let service: BcCommonService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BcCommonService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
