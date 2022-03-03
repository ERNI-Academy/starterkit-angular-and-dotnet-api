import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogsIconSeverityComponent } from './logs-icon-severity.component';

describe('LogsIconSeverityComponent', () => {
  let component: LogsIconSeverityComponent;
  let fixture: ComponentFixture<LogsIconSeverityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LogsIconSeverityComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LogsIconSeverityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
