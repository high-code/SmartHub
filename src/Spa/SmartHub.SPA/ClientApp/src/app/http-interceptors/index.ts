import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { JwtTokenInterceptor } from './jwt-interceptor';


export const httpInterceptorProviders = [
    { provide: HTTP_INTERCEPTORS, useClass: JwtTokenInterceptor, multi: true }
]