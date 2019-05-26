import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { DeviceDescription } from '../models/device-description';
import { RegisterDevice } from '../models/register-device';
import { Device } from '../models/device';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class DeviceService {

  devicesUrl = "device"
  registerDeviceUrl = "register";
  baseUrl = "https://localhost:44319/api";

  constructor(private http: HttpClient) 
  {
     
  }

  getDevicesDescs() : Observable<DeviceDescription[]> {
     return this.http.get<DeviceDescription[]>(`${this.baseUrl}/${this.devicesUrl}`);
  }

  getDevice(id: number) : Observable<Device> {
    const url = `${this.baseUrl}/${this.devicesUrl}/${id}`;
    return this.http.get<Device>(url)
       .pipe(
         tap(a => console.log(a))
       );
  }

  registerDevice(registerDevice : RegisterDevice) : Observable<DeviceDescription> {

    let fullUrl = `${this.baseUrl}/${this.devicesUrl}/${this.registerDeviceUrl}`;
    return this.http.post<DeviceDescription>(fullUrl, registerDevice);
  }


  deleteDevice(id: string) {
    let fullUrl = `${this.baseUrl}/${this.devicesUrl}/${id}`
    return this.http.delete(fullUrl);
  }

}
