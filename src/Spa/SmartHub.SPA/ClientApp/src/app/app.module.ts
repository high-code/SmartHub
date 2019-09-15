import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DeviceListComponent } from './device-list/device-list.component';
import { DeviceCardComponent } from './device-list/device-card/device-card.component';
import { RegisterDeviceComponent } from './register-device/register-device.component';
import { HttpClientInMemoryWebApiModule } from 'angular-in-memory-web-api';
import { InMemoryDataService } from './services/in-memory-data-service.service';
import { HttpClientModule } from '@angular/common/http';
import { DevicePageComponent } from './device-page/device-page.component';
import { NotificationBoxComponent } from './notification-box/notification-box.component';
import { NotificationComponent } from './notification/notification.component';
import { NotificationDirective } from './notification-box/notification-directive.directive';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ConfigurationService } from './services/configuration.service';
import { StorageService } from './services/storage.service';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { AuthGuard } from './services/auth-guard.service';
import { AuthComponent } from './auth/auth.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SmarthubProfileComponent } from './smarthub-profile/smarthub-profile.component';

@NgModule({
  declarations: [
    AppComponent,
    DeviceListComponent,
    DeviceCardComponent,
    RegisterDeviceComponent,
    DevicePageComponent,
    NotificationBoxComponent,
    NotificationComponent,
    NotificationDirective,
    UnauthorizedComponent,
    AuthComponent,
    SmarthubProfileComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    NgxSpinnerModule,
    NgbModule
  ],
  providers: [ConfigurationService, StorageService, AuthGuard],
  entryComponents: [NotificationComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
