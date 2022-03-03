import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { EventTypes } from './models/socket-event-api-models';
import { Subject } from 'rxjs';
import * as signalR from '@microsoft/signalr';

const debugSocket = !!localStorage.getItem('DEBUG_SOCKET_MESSAGES');

@Injectable({
  providedIn: 'root',
})
export class SocketService {
  private eventsSubject = new Subject<EventTypes>();
  public readonly events$ = this.eventsSubject.asObservable();

  private connection: signalR.HubConnection;

  constructor() {
    // this.authService.sessionCreated$.subscribe(() => {
    //   // we connect to socket as soon as the user is authenticated
    //   this.connect();
    // });
    this.connect();
  }

  connect(): void {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.apiUrl}/notify`)
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.connection.onclose(error => {
      console.error('SignalR close connection.');

      // restart connection
      this.start();
    });

    this.initializeCallBack();
    this.start();
  }

  private async start() {
    try {
      await this.connection.start();
      this.socketLog('SignalR Connected.');
    } catch (err) {
      console.error(err);
      // retry every 5 sec
      setTimeout(() => this.start(), 5000);
    }
  }

  private initializeCallBack() {
    this.connection.on('ReceiveMessage', (...data) => {
      console.log('signalR message!', data);
    });

    this.connection.on('BroadcastMessage', (type: string, payload: string, ...data) => {
      this.socketLog('signalR broadcast message!', {type, payload, data});

      this.eventsSubject.next({
        type: type as any,
        message: JSON.parse(payload),
      });
    });
  }

  /** This can be used to send messages to API */
  private async invokeHubMethod() {
    const user = '';
    const message = '';
    try {
      await this.connection.invoke('SendMessage', user, message);
    } catch (err) {
      console.error(err);
    }
  }

  disconnect(): void {
    this.connection.stop();
  }

  private socketLog(...data: any[]) {
    if (debugSocket) {
      console.log(...data);
    }
  }

}
