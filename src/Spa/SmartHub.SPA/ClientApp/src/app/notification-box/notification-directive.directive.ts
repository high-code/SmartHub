import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[notification-host]'
})
export class NotificationDirective {

  constructor(public viewContainer : ViewContainerRef) { }

}
