import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { enviroment } from 'src/enviroment/enviroment';
@Injectable({
  providedIn: 'root'
})
export class WebServiceService {
  private hubConnection: signalR.HubConnection | undefined;
  private url = enviroment.WS_MONITOR_BASE_URL;

  private messageSubject = new Subject<string>();

  constructor() {
    this.startConnection();
  }

  private startConnection() {

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.url + '/socket-hub',{
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      }) // Replace with your actual hub URL
      .build();

    this.hubConnection.on('ReceiveMessage', (message: string) => {
      this.messageSubject.next(message);
    });

    this.hubConnection
      .start()
      .then(() => {
        console.log('SignalR connection established');
      })
      .catch((err) => {
        console.error('Error while starting SignalR connection', err);
      });
  }

  getMessageObservable() {
    return this.messageSubject.asObservable();
  }
}

