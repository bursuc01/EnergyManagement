import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Device } from 'src/app/interfaces/device';
import { DeviceCreate } from 'src/app/interfaces/deviceCreate';
import { enviroment } from 'src/enviroment/enviroment';
import { Message } from 'src/app/interfaces/message';
import { UserService } from '../user-service/user.service';
import { HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class DeviceService {
  private linkUrl = enviroment.API_DEVICE_BASE_URL+'/api/Device';
  private userUrl = enviroment.API_DEVICE_BASE_URL+'/api/User';
  constructor(
    private http: HttpClient,
    private userService: UserService
  ) { }

  getUserDeviceList(userId: number): Observable<any> {
    let getUrl = this.userUrl + '?id=' + userId;
    let token = this.userService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    console.log(token);
    return this.http.get<any[]>(getUrl, {headers});
  }

  postDevice(device: DeviceCreate): Observable<any> {
    let token = this.userService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.post<Device>(this.linkUrl, device, { headers });
  }

  deleteDevice(deviceId: number): Observable<any> {
    let token = this.userService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    const deleteUserUrl = this.linkUrl + '/' + deviceId;
    return this.http.delete<any>(deleteUserUrl, {headers});
  }

  linkDeviceToUser(userId: number, deviceId: number): Observable<any> {
    let token = this.userService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    const updateUrl = this.userUrl + '/link?deviceId=' + deviceId + '&userId=' + userId;
    return this.http.put<any>(updateUrl,[],{ headers });
  }

  unlinkDeviceFromUser(userId: number, deviceId: number): Observable<any> {
    let token = this.userService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    const updateUrl = this.userUrl + '/unlink?deviceId=' + deviceId + '&userId=' + userId;
    return this.http.put<any>(updateUrl,[],{ headers });
  }
  
  updateDevice(updatedDeviceData: any): Observable<any> {
    let token = this.userService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.put<any>(this.linkUrl, updatedDeviceData, { headers });
  }

  getDevices(): Observable<any[]> {
    let token = this.userService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.get<any[]>(this.linkUrl, { headers });
  }
}
