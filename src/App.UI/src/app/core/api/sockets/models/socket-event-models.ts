
/**********************
 * GENERIC EVENTS
 **********************/
export const LOGS_NEW_ERROR = 'LOGS_NEW_ERROR';
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

export interface LogsNewErrorEventMessage {
  id: string;
  title: string;
}

export interface LogsNewErrorEvent extends EventMessage<LogsNewErrorEventMessage> {
  type: typeof LOGS_NEW_ERROR;
}

/**********************
 * LOGS_ANOTHER_KIND_OF_ERROR EVENT
 **********************/

export interface LogsAnotherExampleEventMessage {
  id: string;
  title: string;
}

export interface LogsAnotherExampleEvent extends EventMessage<LogsAnotherExampleEventMessage> {
  type: typeof LOGS_ANOTHER_KIND_OF_ERROR;
}


// prettier-ignore
export type EventTypes =
  | LogsNewErrorEvent
  | LogsAnotherExampleEvent
  ;
