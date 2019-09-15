import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'unauthorized',
  templateUrl: './unauthorized.component.html'
})
export class UnauthorizedComponent {

  constructor(private location: Location,private authService: AuthenticationService) { }

   
  login() {
    this.authService.signin();
  }

  goback() {
    this.location.back();
  }
  


}
