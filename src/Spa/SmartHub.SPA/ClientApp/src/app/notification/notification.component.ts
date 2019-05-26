import { Component, OnInit, Input, OnDestroy, EventEmitter } from '@angular/core';
import { INotificationModel } from '../models/notification-model';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.less']
})
export class NotificationComponent implements OnInit {
 
  notificationModel : INotificationModel;
  dismissed : EventEmitter<any> = new EventEmitter<any>();  
  hideTimeoutMs : number = 5000;

  constructor() { }

  ngOnInit() {
    setTimeout(_ => this.dismiss(), this.hideTimeoutMs);
  }

  dismiss():void {
    this.dismissed.emit();
  }
}
