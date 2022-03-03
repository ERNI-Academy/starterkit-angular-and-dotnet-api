import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { DragDropDirective } from './drag-and-drop.directive';


@NgModule({
  imports: [
    CommonModule,
  ],
  declarations: [
    DragDropDirective,
  ],
  exports: [
    DragDropDirective,
  ],
})
export class SharedDirectivesModule {}
