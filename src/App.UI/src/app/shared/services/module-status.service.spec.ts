import { TestBed } from '@angular/core/testing';

import { ModuleStatusService } from './module-status.service';

describe('ModuleStatusService', () => {
  let service: ModuleStatusService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ModuleStatusService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
