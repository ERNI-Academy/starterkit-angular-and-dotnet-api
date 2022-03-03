import { MenuComponent } from './menu.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [MenuComponent],
  imports: [
    CommonModule,
  ],
  exports: [
    MenuComponent,
  ]
})
export class MenuModule { }
