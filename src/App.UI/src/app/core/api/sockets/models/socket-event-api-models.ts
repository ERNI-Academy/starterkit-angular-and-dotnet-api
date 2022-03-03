import { CurrentOperationResponse, Log } from '../../generated';
import { NotificationType } from '../../generated/model/notificationType';


/**********************
 * GENERIC EVENTS
 **********************/
 export const LOGS_NEW_ERROR = NotificationType.Logs;
 export const MODULE_STATUS = NotificationType.ModuleStatus;
 export const LOGS_ANOTHER_KIND_OF_ERROR = 'LOGS_ANOTHER_KIND_OF_ERROR';
// export const UPDATED_NODE = 'UPDATED';
// export const NOTIFIED_NODE = 'NOTIFIED';

export type SockeTEventType =
    typeof LOGS_NEW_ERROR
  | typeof LOGS_ANOTHER_KIND_OF_ERROR
  ;

export interface EventMessage<T> {
  message: T;
  type: SockeTEventType;
}

/**********************
 * LOGS_NEW_ERROR EVENT
 **********************/

 export type LogsNewErrorEventMessageAPI = Log;

 export interface LogsNewErrorEvent extends EventMessage<LogsNewErrorEventMessageAPI> {
   type: typeof LOGS_NEW_ERROR;
 }

/**********************
 * LOGS_ANOTHER_KIND_OF_ERROR EVENT
 **********************/

export interface LogsAnotherExampleEventMessageAPI {
  id: string;
  title: string;
}

export interface LogsAnotherExampleEvent extends EventMessage<LogsAnotherExampleEventMessageAPI> {
  type: typeof LOGS_ANOTHER_KIND_OF_ERROR;
}


// prettier-ignore
export type EventTypes =
  | LogsNewErrorEvent
  | LogsAnotherExampleEvent
  ;
