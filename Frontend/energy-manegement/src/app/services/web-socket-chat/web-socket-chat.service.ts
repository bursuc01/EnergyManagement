import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { enviroment } from 'src/enviroment/enviroment'; // Make sure the path is correct
import * as signalR from '@microsoft/signalr';
import { UserService } from '../user-service/user.service';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WebSocketChatService {
  private chatConnection?: HubConnection;
  private messageSubject = new Subject<string>();

  constructor(private userService: UserService) {}

  createChatConnection() {
    this.chatConnection = new HubConnectionBuilder()
      .withUrl(enviroment.WS_CHAT_BASE_URL + '/chat-hub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .withAutomaticReconnect()
      .build();

    this.chatConnection
      .start()
      .then(() => {
        this.addUserConnectionId();
      })
      .catch(error => {
        console.log(error);
      });

      this.receiveMessage();
  }

  stopChatConnection() {
    this.chatConnection?.stop().catch(error => {
      console.log(error);
    });
  }

  async addUserConnectionId() {
    try {
      let username = this.userService.getUserFromToken().name;
      await this.chatConnection?.invoke("AddUserToLogs", username);
      console.log("Method invoked successfully");
    } catch (error) {
      console.log(error);
    }
  }

  async sendMessage(
    sender: String, 
    receiver: String, 
    message: String) {
      try{
        await this.chatConnection?.invoke("SendMessage", sender, receiver, message); 
        console.log("Message has been sent!");
      } catch (error) {
        console.log(error);
      }
  }

  receiveMessage(){
    this.chatConnection?.on('ReceiveMessage', (message: string) => {
      this.messageSubject.next(message);
    });
  }

  getMessageObservable() {
    return this.messageSubject.asObservable();
  }
}
