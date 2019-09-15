import { Injectable } from "@angular/core";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";
import { AuthenticationService } from "./authentication.service";

@Injectable()
export class AuthGuard implements CanActivate {
    
    constructor(private authService: AuthenticationService, private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let isLoggedIn = this.authService.isLoggedInObs();
        isLoggedIn.subscribe(loggedIn => {
            if(!loggedIn)
              this.router.navigate(['unauthorized']);
        });

        return isLoggedIn;
    }
}