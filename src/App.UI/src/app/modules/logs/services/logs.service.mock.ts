import { Public } from '@app/shared/utils/types';
import { Pagination, PaginationResult } from '../../../shared/models/pagination';
import { GeneralEventsService } from '../../../core/services/events/general-events.service';
import { Injectable } from '@angular/core';
import { LogElement, LogSeverity, LogsService, LogStatus } from './logs.service';
import { SnackbarService, SnackBarType } from '@app/shared/services/snackbar.service';

@Injectable({
  providedIn: 'root',
})
export class LogsServiceMock implements Public<LogsService> {
  logs: LogElement[] = [];
  private number = 0;
  private activeLogs: {
    id: string;
    message: string;
  }[] = [];

  // public readonly logsActiveError$: Observable<LogsNewErrorEventMessage>;

  constructor(
    private eventsService: GeneralEventsService,
    private snackbarService: SnackbarService,
  ) {
    /** MOCK LOGS */
    //Array.from({length: 40}, () => Math.floor(Math.random() * 40));
    for (let index = 0; index < 200; index++) {
      const id = this.number++;
      this.logs.push({
        id: `${id}`,
        code: `Code ${id}`,
        module: `Module ${id}`,
        title: `Lore Ipsum Title ${id}`,
        time: new Date(),
        // force id 0,1 pending
        status: (id === 0 || id === 1) ? 'pending' : (['pending', 'normal'] as LogStatus[])[Math.floor(Math.random() * 2)],
        severity: (['error', 'info', 'warn'] as LogSeverity[])[Math.floor(Math.random() * 3)],
        description: `Lore Ipsum Description ${id}`,
        actions: `Lore Ipsum Actions ${id}`,
      });
    }

    this.notificationsSubscribe();
  }
  getActiveLogCount(): number {
    return this.activeLogs.length;
  }

  getLastActiveLog() {
    return this.activeLogs[0];
  }

  async getLogs(page: Pagination<LogElement>): Promise<PaginationResult<LogElement>> {
    return {
      elements: await this.getLogsPage(page),
      totalElements: this.logs.length,
    };
  }

  private async getLogsPage(page: Pagination<LogElement>): Promise<LogElement[]> {
    const currentPage = page.page - 1; // depends if starts at 0 or 1.
    return this.logs
      .slice(currentPage * page.pageSize, (currentPage + 1) * page.pageSize)
      ;
  }

  async getLog(id: string): Promise<LogElement> {
    return this.logs.find((x) => x.id === id);
  }

  async acknowledge(id: string): Promise<void> {
    const log = this.logs.find((x) => x.id === id);
    log.status = 'normal';

    // remove from active list
    const activeLog = this.activeLogs.find((x) => x.id === id);
    if (activeLog) {
      this.activeLogs = this.activeLogs.filter((x) => x.id !== id);
    }
  }

  async acknowledgeAll(): Promise<void> {
    this.logs.forEach(log => log.status = 'normal');

    // delete active logs
    this.activeLogs = [];
  }

  private notificationsSubscribe() {
    this.eventsService.logsNewError$.subscribe((log) => {
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
