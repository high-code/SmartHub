import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-login-board',
  templateUrl: './login-board.component.html',
  styleUrls: ['./login-board.component.css']
})
export class LoginBoardComponent implements OnInit {
  credentials : any = {}

  constructor(private authenticationService: AuthenticationService) { }
  
  ngOnInit() {
  }


  login() {
    this.authenticationService.login(this.credentials.userName, this.credentials.password);
  }

}
