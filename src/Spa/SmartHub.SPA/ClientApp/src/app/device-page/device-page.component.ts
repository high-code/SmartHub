import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap, Route } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Device } from '../models/device';
import { DeviceService } from '../services/device.service';
import { Observable } from 'rxjs';
import * as signalR from '@aspnet/signalr';
@Component({
  selector: 'app-device-page',
  templateUrl: './device-page.component.html',
  styleUrls: ['./device-page.component.less']
})
export class DevicePageComponent implements OnInit {
  
  device: Device;
  measurements: number[];
  connection: signalR.HubConnection;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private deviceService: DeviceService) 
  {
      this.connection = new signalR.HubConnectionBuilder()
         .withUrl("https://localhost:5002/deviceHub")
         .configureLogging(signalR.LogLevel.Debug)
         .build();
      this.measurements = [1];
  }

  ngOnInit() {
     this.route.paramMap.pipe(
       switchMap((params: ParamMap) => this.deviceService.getDevice(+params.get('id')))
     ).subscribe(device => {
        this.device = device;
        this.connection.start()
        .then(() => {
          this.connection.invoke("subscribe", this.device.deviceId)
          this.connection.on("receiveTelemetry", this.receiveTelemetry())
        })
        .catch(error => console.log(error));
     })
  }

  deleteDevice(id: string) {
     this.deviceService.deleteDevice(id)
        .subscribe(_ => this.goBack());
    
  }
  goBack() {
    this.router.navigate(['/devices'])
  }

  
  receiveTelemetry() {
    
    let measurementsArr = this.measurements;
    return function(data: any) {
      measurementsArr.push(data);
      console.log(data);
    }

  }
  



}
