import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { DeviceDescription } from '../models/device-description';
import { RegisterDevice } from '../models/register-device';
import { Device } from '../models/device';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Telemetry } from '../models/telemetry.model';
import { ConfigurationService } from './configuration.service';
@Injectable({
  providedIn: 'root'
})
export class DeviceService {

  baseUrl: string = "";
  devicesUrl: string = "device"
  telemetryUrl: string = "telemetry";
  registerDeviceUrl: string = "register";

  constructor(private http: HttpClient, private configurationService: ConfigurationService) {
    if (this.configurationService.isLoaded) {
        this.baseUrl = this.configurationService.configuration.smartHubApiUrl; 
    }
  }

  getDevicesDescs(): Observable<DeviceDescription[]> {
    return this.http.get<DeviceDescription[]>(`${this.baseUrl}/${this.devicesUrl}`);
  }

  getDevice(id: number): Observable<Device> {
    const url = `${this.baseUrl}/${this.devicesUrl}/${id}`;
    return this.http.get<Device>(url)
      .pipe(
        tap(a => console.log(a))
      );
  }

  getTelemetry(id: number): Observable<Telemetry[]> {
    const url = `${this.baseUrl}/${this.telemetryUrl}/${id}`;
    return this.http.get<Telemetry[]>(url);
  }

  registerDevice(registerDevice: RegisterDevice): Observable<DeviceDescription> {
    const fullUrl = `${this.baseUrl}/${this.devicesUrl}/${this.registerDeviceUrl}`;
    return this.http.post<DeviceDescription>(fullUrl, registerDevice);
  }


  deleteDevice(id: string) {
    const fullUrl = `${this.baseUrl}/${this.devicesUrl}/${id}`
    return this.http.delete(fullUrl);
  }

}
