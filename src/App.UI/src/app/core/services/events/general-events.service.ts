import { Injectable } from '@angular/core';
import { EventTypes, LOGS_NEW_ERROR, LogsNewErrorEventMessageAPI, LogsAnotherExampleEventMessageAPI } from '@app/core/api/sockets/models/socket-event-api-models';
import {
  LogsAnotherExampleEventMessage,
  LogsNewErrorEventMessage,
  LOGS_ANOTHER_KIND_OF_ERROR,
} from '@app/core/api/sockets/models/socket-event-models';
import { SocketService } from '@app/core/api/sockets/socket.service';
import { Observable } from 'rxjs';
import { filter, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class GeneralEventsService {
  public readonly logsNewError$: Observable<LogsNewErrorEventMessage>;
  public readonly logsAnotherExample$: Observable<LogsAnotherExampleEventMessage>;

  constructor(private socketService: SocketService) {
    const socketMessages = this.onMessage();

    /** These filter + mappings can be used as a protective layer against the real types coming from the Socket Service.
     * In case they change too often.
     * Only the "filter" part is used for the moment and the real types from the Sockets are used as an output which could
     * be changed to protect the callees from the Socket service types.
     */

    this.logsNewError$ = socketMessages.pipe(
      filter(event => event.type === LOGS_NEW_ERROR),
      map(event => event.message as LogsNewErrorEventMessageAPI),
      /** Example of a possible map (the same output type is used but could be changed here) */
      map(
        payload =>
          ({
            id: payload.id.toString(),
            title: payload.title,
          } as LogsNewErrorEventMessage)
      )
    );

    this.logsAnotherExample$ = socketMessages.pipe(
      filter(event => event.type === LOGS_ANOTHER_KIND_OF_ERROR),
      map(event => event.message as LogsAnotherExampleEventMessageAPI),
      map(payload => payload as LogsAnotherExampleEventMessage)
    );
  }

  private onMessage(): Observable<EventTypes> {
    return this.socketService.events$;
  }
}
