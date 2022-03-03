import { ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { InputComponent } from './input/input.component';
import { MaterialModule } from './../../material/material.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ValidationErrorComponent } from './validation-error/validation-error.component';
import { NumericDirective } from './directives/numeric.directive';



@NgModule({
  declarations: [ValidationErrorComponent, InputComponent, NumericDirective],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MaterialModule,
    TranslateModule,
  ],
  exports: [
    ValidationErrorComponent,
    InputComponent,
    NumericDirective,
  ]
})
export class InputModule { }
