import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { DeviceService } from '../services/device.service';
import { DeviceDescription } from '../models/device-description';
import { Router } from '@angular/router';
@Component({
  selector: 'app-register-device',
  templateUrl: './register-device.component.html',
  styleUrls: ['./register-device.component.less']
})
export class RegisterDeviceComponent implements OnInit {
  
  registerDeviceForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required])
  });

  constructor(private deviceService: DeviceService,
              private router : Router) { }

  ngOnInit() {
  }


  onSubmit() {

    this.deviceService.registerDevice(this.registerDeviceForm.value)
       .subscribe(_ => this.goBack());
  }


  goBack() {
    this.router.navigate(['/devices'])
  }

}
