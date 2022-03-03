import { TestBed } from '@angular/core/testing';

import { GlobalPendingChangesService } from './global-pending-changes.service';

describe('GlobalPendingChangesService', () => {
  let service: GlobalPendingChangesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GlobalPendingChangesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
