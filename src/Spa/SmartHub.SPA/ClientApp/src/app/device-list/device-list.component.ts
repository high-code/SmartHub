import { Component, OnInit } from '@angular/core';
import { DeviceDescription } from '../models/device-description';
import { DeviceService } from '../services/device.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'device-list',
  templateUrl: './device-list.component.html',
  styleUrls: ['./device-list.component.less']
})
export class DeviceListComponent implements OnInit {

  devicesList: DeviceDescription[];

  constructor(private deviceService: DeviceService,
    private spinnerService: NgxSpinnerService) { }

  ngOnInit() {
    this.getDevices();
  }

  getDevices() {
    this.spinnerService.show("window-spinner")
    this.deviceService.getDevicesDescs()
      .subscribe(devices => {
        this.spinnerService.hide("window-spinner")
        this.devicesList = devices
      });
  }
}
