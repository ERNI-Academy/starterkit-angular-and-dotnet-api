import { Pagination, PaginationResult } from './../../../shared/models/pagination';
import { GeneralEventsService } from './../../../core/services/events/general-events.service';
import { Injectable } from '@angular/core';
import { LogsService as APILogsService } from '@app/core/api/generated/api/logs.service';
import { map } from 'rxjs/operators';
import { Log, LogType } from '@app/core/api/generated';
import { SnackbarService, SnackBarType } from '@app/shared/services/snackbar.service';

export type LogStatus = 'pending' | 'normal';
export type LogSeverity = 'info' | 'error' | 'warn';

export interface LogElement {
  id: string;
  code: string;
  module: string;
  title: string;
  time: Date;
  status: LogStatus;
  severity: LogSeverity;
  description: string;
  actions?: string;
}


const fromAPILogToUILog = (log: Log): LogElement => ({
  code: log.code,
  description: log.description,
  id: log.id.toString(),
  module: log.module,
  time: new Date(Date.parse(log.date)),
  title: log.title,
  actions: log.actions,
  status: log.isAcknowledged ? 'normal' : 'pending',
  severity: log.type === LogType.Error ? 'error' :
    log.type === LogType.Warning ? 'warn' :
    'info',
});

@Injectable({
  providedIn: 'root',
})
export class LogsService {
  private activeLogs: {
    id: string;
    message: string;
  }[] = [];

  constructor(
    private eventsService: GeneralEventsService,
    private snackbarService: SnackbarService,
    private apiLogsService: APILogsService,
  ) {
    this.notificationsSubscribe();
  }

  getLastActiveLog() {
    return this.activeLogs[0];
  }

  getActiveLogCount() {
    return this.activeLogs.length;
  }

  async getLogs(page: Pagination<LogElement>): Promise<PaginationResult<LogElement>> {
    return this.apiLogsService.logsGet({
      page: page.page,
      pageSize: page.pageSize,
    }).pipe(
      map(result => ({
          totalElements: result.totalElements,
          elements: result.elements.map(fromAPILogToUILog),
      })),
    ).toPromise();
  }

  async getLog(id: string): Promise<LogElement> {
    // return this.logs.find((x) => x.id === id);
    return this.apiLogsService.logsIdGet({
      id: Number(id),
    }).pipe(
      map(fromAPILogToUILog)
    ).toPromise();
  }

  async acknowledge(id: string): Promise<void> {
    await this.apiLogsService.logsSetAckIdPost({
      id: Number(id),
    }).toPromise();
  }

  async acknowledgeAll(): Promise<void> {
    await this.apiLogsService.logsSetAckAllPost().toPromise();

    // delete active logs
    this.activeLogs = [];
  }

  private notificationsSubscribe() {
    this.eventsService.logsNewError$.subscribe(log => {
      // FILO queue
      this.activeLogs = [
        {
          id: log.id,
          message: log.title,
        },
        ...this.activeLogs,
      ];

      this.snackbarService.openSnackBar(
        `Error log: ${log.title}`,
        SnackBarType.error,
      );

    });
  }
}
