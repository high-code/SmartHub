import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { Observable, of} from 'rxjs';
import { map } from 'rxjs/operators';
@Component({
  selector: 'smarthub-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent {


  userName: string;
  isLoggedIn$: Observable<boolean>;
  authService: AuthenticationService;
  constructor(private authenticationService: AuthenticationService) {
    this.authService = authenticationService;
    this.isLoggedIn$ = authenticationService.isLoggedInObs();
  }

  ngOnInit() {
    this.authenticationService.getUser().pipe(
      map(u => u.profile.name)
    )
    .subscribe(x => this.userName = x);
  }
  login() {
    this.authService.signin();
  }
  logout() {
    this.authService.signout();
  }
}
