import { Injectable } from '@angular/core';

import { 
    HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../services/authentication.service';

@Injectable()
export class JwtTokenInterceptor implements HttpInterceptor {
  

    constructor(private authService: AuthenticationService) {
    }

    intercept(request: HttpRequest<any>,next: HttpHandler ) 
       : Observable<HttpEvent<any>>{
        
        let currentUser = this.authService.currentUser

        if(currentUser && currentUser.access_token) {
            request = request.clone({
                setHeaders : {
                    "Authorization" : `Bearer ${currentUser.access_token}` 
                }
            })
        }

        return next.handle(request);
    }
}
