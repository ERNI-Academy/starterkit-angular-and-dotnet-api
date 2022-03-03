import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Tab2Component } from './tab2.component';

describe('Tab2Component', () => {
  let component: Tab2Component;
  let fixture: ComponentFixture<Tab2Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ Tab2Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(Tab2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
