import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { Observable} from 'rxjs';
import { map } from 'rxjs/operators';
@Component({
  selector: 'smarthub-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent {


  userName: Observable<string>;
  isLoggedIn$: Observable<boolean>;
  authService: AuthenticationService;
  constructor(private authenticationService: AuthenticationService) {
    this.authService = authenticationService;
    this.isLoggedIn$ = authenticationService.isLoggedInObs();
    this.userName = authenticationService.getUser().pipe(
      map(u => u.profile.name)
    );
  }

  login() {
    this.authService.signin();
  }
  logout() {
    this.authService.signout();
  }
}
