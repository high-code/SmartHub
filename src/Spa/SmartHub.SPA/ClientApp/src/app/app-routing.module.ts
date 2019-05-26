import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DeviceListComponent } from './device-list/device-list.component';
import { RegisterDeviceComponent } from './register-device/register-device.component';
import { DevicePageComponent } from './device-page/device-page.component';

const routes: Routes = [
  { path: "", redirectTo: "devices", pathMatch: "full"},
  { path: "devices", component: DeviceListComponent, pathMatch: "full"},
  { path: "registerDevice", component: RegisterDeviceComponent, pathMatch: "full"},
  { path: "devicePage/:id", component: DevicePageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
