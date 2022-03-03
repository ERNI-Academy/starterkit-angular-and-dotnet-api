import { TranslateModule } from '@ngx-translate/core';
import { MaterialModule } from './../../material/material.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OperationStatusComponent } from './operation-status.component';
import { AlertStatusIconModule } from '../alert-status-icon/alert-status-icon.module';



@NgModule({
  declarations: [OperationStatusComponent],
  imports: [
    CommonModule,
    MaterialModule,
    AlertStatusIconModule,
    TranslateModule,
  ],
  exports: [OperationStatusComponent]
})
export class OperationStatusModule { }
