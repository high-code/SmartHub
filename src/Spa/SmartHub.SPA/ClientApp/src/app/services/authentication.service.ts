import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, from} from 'rxjs';
import { tap,map } from 'rxjs/operators';
import { UserManager, User } from 'oidc-client';
import { environment } from './../../environments/environment';
const settings: any = {
  authority: 'https://localhost:44338',
  client_id: 'spa',
  redirect_uri: "https://localhost:44332/auth.html",
  response_type: 'id_token token',
  scope: "openid profile",
  silent_redirect_uri: 'http://localhost:44332/silent-renew.html',
  automaticSilentRenew: true,
  accessTokenExpiringNotificationTime: 4,

  filterProtocolClaims: true,
  loadUserInfo: true
}

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  userManager : UserManager = new UserManager(settings);
  loggedIn = false;
  currentUser: User;
  userLoadedEvent: EventEmitter<User> = new EventEmitter<User>();

  constructor(private http: HttpClient) {
    this.userManager.getUser()
       .then((user) => {
         if(user) {
          this.loggedIn = true;
          this.currentUser = user;
          this.userLoadedEvent.emit(user);
         }
         else {
           this.loggedIn = false;
         }
       })
       .catch((err) => {
         this.loggedIn = false;
       });

       this.userManager.events.addUserLoaded((user) => {
         this.currentUser = user;
         this.loggedIn = !(user === undefined);
       });

       this.userManager.events.addUserUnloaded(e => {
         this.loggedIn = false;
       })
  }

  getUser() : Observable<User> {
     return from(this.userManager.getUser()).pipe(
       tap((val) => {
         if(!environment.production) {
            console.log("fetching user")
       } 
      })
     )
  }

  isLoggedInObs() : Observable<boolean> 
  {
     return from(this.userManager.getUser())
       .pipe(map<User, boolean>(user => {
         if(user)
           return true;
         return false;
       }))
  }
  signin(){
    this.userManager.signinRedirect().then(() => {
      console.log("signinRedirect done")
    })
    .catch((err) => {
      console.log(err)
    })
  }


  signout() {
    this.userManager.getUser().then(user => {
      return this.userManager.signoutRedirect({id_token_hint: user.id_token})
        .then(resp => {
          console.log('signed out', resp);
          setTimeout(() => {
            console.log("testing");
          }, 5000);
        })
        .catch(function(err) {
          console.log(err);
        })
    })
  }

  
     
     
  }

