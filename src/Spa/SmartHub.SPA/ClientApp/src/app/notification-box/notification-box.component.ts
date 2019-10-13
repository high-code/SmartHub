import { Component, OnInit, ViewChild, ComponentFactoryResolver, ViewContainerRef, EventEmitter } from '@angular/core';
import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import { NotificationDirective } from './notification-directive.directive';
import { INotificationModel } from '../models/notification-model';
import { NotificationComponent } from '../notification/notification.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'notification-box',
  templateUrl: './notification-box.component.html',
  styleUrls: ['./notification-box.component.scss']
})
export class NotificationBoxComponent implements OnInit {
  
  connection: signalR.HubConnection;

  constructor(private toastr: ToastrService) {
    this.connection = new HubConnectionBuilder()
         .withUrl("http://localhost:5002/notificationHub")
         .configureLogging(LogLevel.Debug)
         .build();
   }

  ngOnInit() {
    this.connection.start()
       .then(_ => this.connection.on("notify", this.notify));

  }

  notify(notification: INotificationModel) {
     this.toastr.info(notification.body, notification.header,{positionClass: "toast-bottom-right",
      closeButton: true, tapToDismiss: false, timeOut: 5000});
  }

}
