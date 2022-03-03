import {
  Component,
  OnInit,
  AfterViewInit,
  ViewChild,
  OnDestroy,
} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { SnackbarService } from '@app/shared/services/snackbar.service';
import { Subscription } from 'rxjs';
import { tap } from 'rxjs/operators';
import { LogElement, LogsService } from '../../services/logs.service';
import { LogsDataSource } from './logs.datasource';

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

@Component({
  selector: 'app-logs-table',
  templateUrl: './logs-table.component.html',
  styleUrls: ['./logs-table.component.scss'],
})
export class LogsTableComponent implements OnInit, OnDestroy, AfterViewInit {
  private subscriptions: Subscription[] = [];

  displayedColumns: string[] = ['code', 'module', 'time', 'title'];
  dataSource: LogsDataSource;

  get totalElements() {
    return this.dataSource.totalElements;
  }
  currentPage = 0;
  currentPageSize = 10;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private logsService: LogsService,
    private route: ActivatedRoute,
    private router: Router,
    private snackbarService: SnackbarService,
    ) {}

  ngOnInit(): void {
    this.dataSource = new LogsDataSource(this.logsService);
    this.subscriptions.push(
      this.route.queryParams
        .pipe(
          tap((params) => {
            this.currentPage = Number(params.page ?? 0);
            this.dataSource.loadLogs(this.currentPage, this.currentPageSize);
            if (this.paginator) {
              this.paginator.pageIndex = this.currentPage;
            }
          })
        )
        .subscribe()
    );
  }

  ngAfterViewInit() {
    // this.dataSource.paginator = this.paginator;
    this.paginator.page.pipe(tap(() => this.loadLogsPage())).subscribe();
  }

  ngOnDestroy() {
    this.subscriptions.forEach((s) => s.unsubscribe());
  }

  loadLogsPage() {
    this.currentPage = this.paginator.pageIndex;
    this.dataSource.loadLogs(
      this.paginator.pageIndex,
      this.paginator.pageSize
    );
  }

  viewLog(row: LogElement) {
    this.router.navigate(['/logs/view-log', row.id], {
      queryParams: { page: this.paginator.pageIndex },
    });
  }

  async acknowledgeAll() {
    await this.logsService.acknowledgeAll();
    this.dataSource.reload();
    this.snackbarService.defaultSuccess();
  }

}
