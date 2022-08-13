import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";

@Injectable()
export class LoginGuard implements CanActivate {
  constructor(
    private router: Router,
  ) {
    console.log("LoginGuard")

  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
    boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    console.log("gaurd");
    console.log(this.router.createUrlTree(['admin/auth']).toString());
    return this.router.createUrlTree(['admin/auth']);
    //return this.store.select('loginState').pipe(
    //  take(1),
    //  map(loginState => {
    //    console.log(loginState);
    //    return loginState.user;
    //  }),
    //  map(user => {
    //    console.log(user);
    //    const isAuth = !!user;
    //    if (isAuth) {
    //      return true;
    //    }
    //    return this.router.createUrlTree(['/auth']);
    //  })
    //);
    }

}
