import { Injectable } from '@angular/core';
import { InMemoryDbService } from 'angular-in-memory-web-api';
import { DeviceDescription } from '../models/device-description';

@Injectable({
  providedIn: 'root'
})
export class InMemoryDataService implements InMemoryDbService {
  createDb() {
    const devicesDesc = [
        { id: 1, name: "Arduino", description : "My first device"},
        { id: 2, name: "NodeMCU", description : "My second device"},
        { id: 3, name: "STM32", description : "My third device"},
        { id: 4, name: "Arduino Nano", description : "A fourth device"}
    ];

    const devices = [
      { id: 1, name: "Arduino", description : "My first device", deviceId: "7b6c4350-de4a-420a-a782-65e8ea331487"},
      { id: 2, name: "NodeMCU", description : "My second device"},
      { id: 3, name: "STM32", description : "My third device"},
      { id: 4, name: "Arduino Nano", description : "A fourth device"}
  ];

    return {devicesDesc,devices};
  }

  genId(devices: DeviceDescription[]) : number {
      return devices.length > 0 ? Math.max(...devices.map(device => device.id)) + 1 : 11;
  }
}
