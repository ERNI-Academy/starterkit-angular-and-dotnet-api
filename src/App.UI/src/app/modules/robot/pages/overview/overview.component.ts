import { Component, OnInit } from '@angular/core';
import { Status } from '@app/shared/component/alert-status-icon/alert-status-icon.component';
import { baseStates, States } from '@app/shared/component/operation-status/operation-status.component';
import { ModuleStatusService } from '@app/shared/services/module-status.service';
import { settimeoutPromise } from '@app/shared/utils/helper.functions';
import { marker } from '@biesbjerg/ngx-translate-extract-marker';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss']
})
export class OverviewComponent implements OnInit {

  generalStatus = Status.Clear;

  paintGeneralStatus: Status;

  UVTOperationTitle: string = marker('overview.UVT-operation');
  UVTOperationName: string;
  UVTOperationStates: Observable<States>;

  CVGOperationTitle: string = marker('overview.CVG-operation');
  CVGOperationName: string;
  CVGOperationStates: Observable<States>;

  constructor(
    private moduleStatusService: ModuleStatusService,
  ) { }

  ngOnInit(): void {
    this.UVTOperationStates = this.moduleStatusService.uvtStatus$;
    this.CVGOperationStates = this.moduleStatusService.cvgStatus$;

    this.UVTOperationName = 'Moving robot...';
    this.CVGOperationName = 'Rotating ripper...';

    this.simulateBlinkingState();

    /**
     * Simulate change of general state
     */
    setTimeout(() => {
      this.generalStatus = Status.Critical;
    }, 5000);
  }

  private simulateBlinkingState(): void {
    const period = 1000;

    /**
     * Example:
     * 1000 seconds showing 'this.generalStatus'
     * 1000 seconds showing nothing
     * Start again.
     */
    const blinkState = async (state: Status) => {
      this.paintGeneralStatus = state;

      await settimeoutPromise(() => {
        this.paintGeneralStatus = undefined;
      }, period);

      await settimeoutPromise(() => {
        // start again
        blinkState(this.generalStatus);
      }, period);
    };

    blinkState(this.generalStatus);
  }

  alarmStateClass(): any {
    return {
      alarm: this.paintGeneralStatus !== undefined,
      clear: this.paintGeneralStatus === Status.Clear,
      indeterminate: this.paintGeneralStatus === Status.Indeterminate,
      warning: this.paintGeneralStatus === Status.Warning,
      critical: this.paintGeneralStatus === Status.Critical,
    };
  }

}
