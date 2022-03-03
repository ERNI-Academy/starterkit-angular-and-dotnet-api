import { map, filter, tap, switchMap, flatMap, mergeMap } from 'rxjs/operators';
import { LogElement } from '../../services/logs.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { LogsService } from '../../services/logs.service';
import { Observable } from 'rxjs';
import { SnackbarService, SnackBarType } from '@app/shared/services/snackbar.service';

@Component({
  templateUrl: './view-log.component.html',
  styleUrls: ['./view-log.component.scss']
})
export class ViewLogComponent implements OnInit {

  logId$: Observable<string>;

  selectedLog: LogElement;
  private commingFromPage: number;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private logsService: LogsService,
    private snackbarService: SnackbarService,
    ) { }

  ngOnInit(): void {
    this.logId$ = this.route.paramMap.pipe(
      map(x => x.get('id')),
      filter(x => x !== undefined),
      switchMap(id => this.route.queryParams.pipe(
        map(params => ({id, params}))
      )),
      tap(params => {
        this.getSelectedLog(params.id);
        this.commingFromPage = params.params.page;
      }),
      map(params => params.id),
    );
  }

  private async getSelectedLog(logId: string) {
    try {
      this.selectedLog = await this.logsService.getLog(logId);
    } catch (error) {
      this.snackbarService.openSnackBar(
        'Error loading log...',
        SnackBarType.error,
      );
    }
  }

  acknowledge(logId: string) {
    this.logsService.acknowledge(logId);
    this.selectedLog.status = 'normal';
  }

  goToLogs(): void {
    const queryParams: Params = this.commingFromPage ?
      { page: this.commingFromPage } : null;
    this.router.navigate(['/logs'], { queryParams });
  }

}
