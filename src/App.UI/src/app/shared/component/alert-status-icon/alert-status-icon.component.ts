import { Component, Input, ChangeDetectionStrategy } from '@angular/core';

export enum Status {
  Clear,
  Indeterminate,
  Warning,
  Critical,
}

@Component({
  selector: 'app-alert-status-icon',
  templateUrl: './alert-status-icon.component.html',
  styleUrls: ['./alert-status-icon.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AlertStatusIconComponent {
  @Input() status: Status;
  /** Dimension in pixels */
  @Input() dimensions: number;

  getStyle(): any {
    if (this.dimensions) {
      return {
        'height.px': this.dimensions,
        'width.px': this.dimensions,
      };
    }
  }

  alarmStateClass(): any {
    return {
      clear: this.status === Status.Clear,
      indeterminate: this.status === Status.Indeterminate,
      warning: this.status === Status.Warning,
      critical: this.status === Status.Critical,
    };
  }
}
