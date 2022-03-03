import { Injectable } from '@angular/core';
import { ReplaySubject, Subject } from 'rxjs';
import { Status } from '../component/alert-status-icon/alert-status-icon.component';
import { baseStates, States } from '../component/operation-status/operation-status.component';

@Injectable({
  providedIn: 'root'
})
export class ModuleStatusService {
  private cvgStatusSubject = new ReplaySubject<States>(1);
  public readonly cvgStatus$ = this.cvgStatusSubject.asObservable();

  private uvtStatusSubject = new ReplaySubject<States>(1);
  public readonly uvtStatus$ = this.uvtStatusSubject.asObservable();

  constructor() {

    this.uvtStatusSubject.next({
      ...baseStates,
      idle: Status.Clear,
      ready: Status.Clear,
    });

    this.cvgStatusSubject.next({
      ...baseStates,
      idle: Status.Clear,
      ready: Status.Clear,
    });

  }
}
