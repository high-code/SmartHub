import { Component, OnInit } from '@angular/core';
import { DeviceDescription } from '../models/device-description';
import { DeviceService } from '../services/device.service';

@Component({
  selector: 'device-list',
  templateUrl: './device-list.component.html',
  styleUrls: ['./device-list.component.less']
})
export class DeviceListComponent implements OnInit {

  devicesList: DeviceDescription[];

  constructor(private deviceService : DeviceService) { }

  ngOnInit() {
    this.getDevices();
  }

  getDevices() {
    this.deviceService.getDevicesDescs()
      .subscribe(devices => this.devicesList = devices);
  }
}
