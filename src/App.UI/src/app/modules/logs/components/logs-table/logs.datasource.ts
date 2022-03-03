import { CollectionViewer } from '@angular/cdk/collections';
import { DataSource } from '@angular/cdk/table';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { LogElement, LogsService } from '../../services/logs.service';

export class LogsDataSource implements DataSource<LogElement> {
  private logsSubject = new BehaviorSubject<LogElement[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);

  public loading$ = this.loadingSubject.asObservable();

  totalElements: number;

  private lastPageIndex: number;
  private lastPageSize: number;

  constructor(private logsService: LogsService) {}

  connect(collectionViewer: CollectionViewer): Observable<LogElement[]> {
    return this.logsSubject.asObservable();
  }

  disconnect(collectionViewer: CollectionViewer): void {
    this.logsSubject.complete();
    this.loadingSubject.complete();
  }

  async loadLogs(pageIndex: number, pageSize: number) {
    this.lastPageIndex = pageIndex;
    this.lastPageSize = pageSize;

    this.loadingSubject.next(true);

    const page = await this.logsService.getLogs({
      page: pageIndex + 1,
      pageSize,
      orderByDescendant: 'time',
    });

    this.logsSubject.next(page.elements);
    this.totalElements = page.totalElements;
  }

  async reload() {
    await this.loadLogs(this.lastPageIndex, this.lastPageSize);
  }
}
