import { Component, OnInit, ChangeDetectionStrategy, Input } from '@angular/core';
import { Status } from '../alert-status-icon/alert-status-icon.component';



export const baseStates = {
  idle: Status.Indeterminate,
  ready: Status.Indeterminate,
  search: Status.Indeterminate,
  sync: Status.Indeterminate,
  link: Status.Indeterminate,
  reset: Status.Indeterminate,
};

export type States = typeof baseStates;



@Component({
  selector: 'app-operation-status',
  templateUrl: './operation-status.component.html',
  styleUrls: ['./operation-status.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class OperationStatusComponent implements OnInit {

  @Input() title: string;
  @Input() operationName: string;
  @Input() states: States;

  constructor() { }

  ngOnInit(): void {
    this.states = this.states || baseStates;
  }

}
