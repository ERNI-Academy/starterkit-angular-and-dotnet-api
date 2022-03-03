import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScrollableOverflowWrapperComponent } from './scrollable-overflow-wrapper.component';

@NgModule({
  declarations: [
    ScrollableOverflowWrapperComponent,
  ],
  imports: [
    CommonModule,
  ],
  exports: [
    ScrollableOverflowWrapperComponent,
  ]
})
export class ScrollableOverflowWrapperModule { }
