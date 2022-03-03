import { LogsNewErrorEventMessageAPI } from '@app/core/api/sockets/models/socket-event-api-models';
import { Injectable } from '@angular/core';
import { EventTypes, LOGS_NEW_ERROR } from './models/socket-event-api-models';
import { Subject } from 'rxjs';
import { SocketService } from './socket.service';
import { Public } from '@app/shared/utils/types';
import { settimeoutPromise } from '@app/shared/utils/helper.functions';

const debugSocket = !!localStorage.getItem('DEBUG_SOCKET_MESSAGES');

@Injectable({
  providedIn: 'root',
})
export class MockSocketService implements Public<SocketService> {
  private eventsSubject = new Subject<EventTypes>();
  public readonly events$ = this.eventsSubject.asObservable();

  client: any;

  constructor() {
    // mock a log after 10 seconds
    // this.mockLogEvents();

  }
  connect(): void {
    throw new Error('Method not implemented.');
  }
  disconnect(): void {
    throw new Error('Method not implemented.');
  }
  connectToTopics(): void {
    throw new Error('Method not implemented.');
  }

  private async mockLogEvents() {
    await settimeoutPromise(() => {
      this.eventsSubject.next({
        type: LOGS_NEW_ERROR,
        message: {
          Id: 0,
          Title: `message for log 0`
        } as LogsNewErrorEventMessageAPI
      });
    }, 10000);
    await settimeoutPromise(() => {
      this.eventsSubject.next({
        type: LOGS_NEW_ERROR,
        message: {
          Id: 1,
          Title: `message for log 1`
        } as LogsNewErrorEventMessageAPI
      });
    }, 10000);
  }

}
