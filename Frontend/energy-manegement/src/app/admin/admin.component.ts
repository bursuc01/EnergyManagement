import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user-service/user.service';
import { DeviceService } from '../services/device-service/device.service';
import { User } from '../interfaces/user';
import { Device } from '../interfaces/device';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit{
  userList: any[] = [];
  deviceList: any[] = []

  constructor(
    private userService: UserService,
    private deviceService: DeviceService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userService.getUsers().subscribe(
      (data) => {
        this.userList = data.sort((a, b) => a.id - b.id);
      }
    );
    
    this.deviceService.getDevices().subscribe(
      (data) => {
        this.deviceList = data.sort((a, b) => a.id - b.id);
      }
    )
  }

  redirect(route: string): void {
    const givenRoute = ["/" + route];
    console.log(givenRoute);
    this.router.navigate(givenRoute);
  } 

  deleteDevice(deviceId: number): void {
    this.deviceService.deleteDevice(deviceId).subscribe(
      () => {
        window.location.reload();
      }
    );
  }

  updateDevice(device: Device): void {
    console.log(device.userId);
    this.deviceService.updateDevice(device).subscribe();
  }

  deleteUser(userId: number): void {
    this.userService.deleteUser(userId).subscribe(
      () => {
        window.location.reload();
      }
    );
  }

  updateUser(user: User): void {
    this.userService.updateUser(user).subscribe();
  }
}
