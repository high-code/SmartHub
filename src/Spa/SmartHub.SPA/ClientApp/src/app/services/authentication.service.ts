import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable} from 'rxjs';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http : HttpClient) { }

  login(username: string, password: string) {
     return this.http.post<any>('api/authenticate', {username:username, password:password})
     
  }

  logout() {
  
     
  }




}
