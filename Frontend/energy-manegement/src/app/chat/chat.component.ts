import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { OnInit } from '@angular/core';
import { UserService } from '../services/user-service/user.service';
import { WebSocketChatService } from '../services/web-socket-chat/web-socket-chat.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})

export class ChatComponent implements OnInit{
  public messages: string[] = [];
  public user = this.chatService.getUserFromToken();


  constructor(
    private location: Location,
    private chatService: UserService,
    private webSocketService: WebSocketChatService
  ) {}

  ngOnInit(): void {
    this.webSocketService.createChatConnection();

    this.webSocketService.getMessageObservable().subscribe((data) => {
      this.messages.push(data);
      console.log(this.messages);
    })
  }

  sendMessage(message: HTMLInputElement) {
    let sender = this.chatService.getUserFromToken().name;
    //this.messages.push(sender + " : " + message.value);

    if (sender !== "Koke") {
      this.webSocketService.sendMessage(sender, "Koke", message.value);
      console.log(this.messages);
    }
    else {
      this.webSocketService.sendMessage(sender, "Horea", message.value);
      console.log(this.messages);
    }

    message.value = "";
  }

  redirect() {
    this.location.back();
  }
}
