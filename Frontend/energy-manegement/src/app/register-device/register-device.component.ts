import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DeviceService } from '../services/device-service/device.service';

@Component({
  selector: 'app-register-device',
  templateUrl: './register-device.component.html',
  styleUrls: ['./register-device.component.scss']
})
export class RegisterDeviceComponent {
  formData = {
    description: '',
    address: '',
    maximumHourlyEnergyConsumption: 0
  };

  constructor(
    private router: Router,
    private deviceService: DeviceService
  ) {}

  createDevice() {
    this.deviceService.postDevice(this.formData).subscribe(
      () => {
        this.goBack();
      }
    );
  }

  goBack() {
    this.router.navigate(["/admin"]);
  }
}
