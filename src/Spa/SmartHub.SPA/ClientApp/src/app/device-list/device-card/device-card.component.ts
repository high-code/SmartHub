import { Component, OnInit, Input } from '@angular/core';
import { DeviceDescription } from '../../models/device-description';

@Component({
  selector: 'device-card',
  templateUrl: './device-card.component.html',
  styleUrls: ['./device.component.scss']
})
export class DeviceCardComponent implements OnInit {
  

  @Input() device : DeviceDescription

  constructor() { }

  ngOnInit() {
  }

}
