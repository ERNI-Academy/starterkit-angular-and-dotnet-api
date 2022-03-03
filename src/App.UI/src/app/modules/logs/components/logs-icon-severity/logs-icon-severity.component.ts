import { Component, Input, OnInit } from '@angular/core';
import { LogSeverity } from '../../services/logs.service';

@Component({
  selector: 'app-logs-icon-severity',
  templateUrl: './logs-icon-severity.component.html',
  styleUrls: ['./logs-icon-severity.component.scss']
})
export class LogsIconSeverityComponent implements OnInit {

  @Input() severity: LogSeverity;
  @Input() sizeClass: 'small' | 'medium' | 'large' = 'small';

  constructor() { }

  ngOnInit(): void {
  }

  getSeverityMatIcon(): string {
    if (this.severity === 'info') {
      return 'info';
    }
    if (this.severity === 'warn') {
      return 'warning';
    }
    return 'error';
  }

  getSeverityClass() {
    return {
      info: this.severity === 'info',
      warn: this.severity === 'warn',
      error: this.severity === 'error',
    };
  }

}
