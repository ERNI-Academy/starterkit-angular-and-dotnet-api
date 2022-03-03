import { SharedModule } from './../../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogsTableComponent } from './logs-table/logs-table.component';
import { LogsIconSeverityComponent } from './logs-icon-severity/logs-icon-severity.component';

@NgModule({
  // eslint-disable-next-line prettier/prettier
  imports: [
    CommonModule,
    SharedModule,
  ],
  exports: [
    LogsTableComponent,
    LogsIconSeverityComponent,
  ],
  declarations: [
    LogsTableComponent,
    LogsIconSeverityComponent,
  ],
})
export class LogsComponentsModule {}
