import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpEvent, HttpHandler } from '@angular/common/http';
import { Observable } from 'rxjs';

export class FakeBackend implements HttpInterceptor {


    intercept(request : HttpRequest<any>, handler: HttpHandler ) : Observable<HttpEvent<any>> {
        return
    }
}