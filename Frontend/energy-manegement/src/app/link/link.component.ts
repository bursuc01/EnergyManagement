import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DeviceService } from '../services/device-service/device.service';

@Component({
  selector: 'app-link',
  templateUrl: './link.component.html',
  styleUrls: ['./link.component.scss']
})
export class LinkComponent {
  userId: string = '';
  deviceId: string = '';

  constructor(
    private router: Router,
    private deviceService: DeviceService
  ) {}

  linkDevice() {
    this.deviceService.linkDeviceToUser(
      Number.parseInt(this.userId), 
      Number.parseInt(this.deviceId)).subscribe(
        () => {
          this.router.navigate(['/admin']);
        }
      );;
  }

  unlinkDevice() {
    this.deviceService.unlinkDeviceFromUser(
      Number.parseInt(this.userId), 
      Number.parseInt(this.deviceId)).subscribe(
        () => {
          this.router.navigate(['/admin']);
        }
      );
  }

  goBack(): void {
    this.router.navigate(["/admin"]);
  }
}