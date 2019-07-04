import { Injectable } from '@angular/core';

import { 
    HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class JwtTokenInterceptor implements HttpInterceptor {
  

    intercept(request: HttpRequest<any>,next: HttpHandler ) 
       : Observable<HttpEvent<any>>{
        
        let currentUser = JSON.parse(localStorage.getItem("currentUser"))

        if(currentUser && currentUser.token) {
            request = request.clone({
                setHeaders : {
                    "Authentication" : currentUser.token
                }
            })
        }

        return next.handle(request);
    }
}