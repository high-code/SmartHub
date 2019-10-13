import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DeviceListComponent } from './device-list/device-list.component';
import { RegisterDeviceComponent } from './register-device/register-device.component';
import { DevicePageComponent } from './device-page/device-page.component';
import { AuthGuard } from './services/auth-guard.service';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { SmarthubProfileComponent } from './smarthub-profile/smarthub-profile.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  { path: "", redirectTo: "devices", pathMatch: "full", canActivate: [AuthGuard]},
  { path: "devices", component: DeviceListComponent, pathMatch: "full", canActivate : [AuthGuard]},
  { path: "registerDevice", component: RegisterDeviceComponent, pathMatch: "full", canActivate : [AuthGuard]},
  { path: "devicePage/:id", component: DevicePageComponent, canActivate : [AuthGuard]},
  { path: "unauthorized", component: UnauthorizedComponent},
  { path: "profile", component: SmarthubProfileComponent, canActivate : [AuthGuard]},
  { path: "register", component: RegisterComponent}
];
 
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
