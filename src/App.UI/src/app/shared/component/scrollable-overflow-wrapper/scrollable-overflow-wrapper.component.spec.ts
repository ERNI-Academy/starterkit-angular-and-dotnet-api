import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScrollableOverflowWrapperComponent } from './scrollable-overflow-wrapper.component';

describe('ScrollableOverflowWrapperComponent', () => {
  let component: ScrollableOverflowWrapperComponent;
  let fixture: ComponentFixture<ScrollableOverflowWrapperComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ScrollableOverflowWrapperComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ScrollableOverflowWrapperComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
