import { TestBed } from '@angular/core/testing';

import { GeneralEventsService } from './general-events.service';

describe('GeneralEventsService', () => {
  let service: GeneralEventsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GeneralEventsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
