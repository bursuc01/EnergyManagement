import { Component } from '@angular/core';
import { DeviceService } from '../services/device-service/device.service';
import { UserService } from '../services/user-service/user.service';
import { WebServiceService } from '../services/web-service/web-service.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  deviceList: any[] = [];
  username: string = '';
  
  messages: any[] = [];
  lastMessage: any;

  constructor(
    private deviceService: DeviceService,
    private userService: UserService,
    private signalRService: WebServiceService
  ) {}

  ngOnInit() {
    const user = this.userService.getUserFromToken();
    this.username = user.name;
    this.deviceService.getUserDeviceList(user.id).subscribe(
      (data) => {
        this.deviceList = data;
        console.log(this.deviceList);
      }
    );

    this.signalRService.getMessageObservable().subscribe((message) => {
      this.messages.push(message);
    });
  }

  socketCheck(){
    const message = this.messages.pop();
    if(message !== undefined) {
      this.lastMessage = message;
      this.makeNotification('visible');
    }
    console.log(this.lastMessage);
    return (this.messages.length == 0)
  }

  makeNotification(value: string) {
    let doc = document.getElementById("notification");
    if(doc) {
      doc.style.visibility = value;
    }
  }

}
