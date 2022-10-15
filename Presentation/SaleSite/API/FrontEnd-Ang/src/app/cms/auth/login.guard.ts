import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Store } from "@ngrx/store";
import { Observable } from "rxjs";
import { map, take } from "rxjs/operators";
import * as fromCmsApp from "../store/cms.reducer"
import * as fromLoginAction from "../auth/store/login.action"

@Injectable()
export class LoginGuard implements CanActivate {
  constructor(
    private router: Router, private store: Store<fromCmsApp.CmsState<any>>
  ) {

  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
    boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    return this.store.select('loginState').pipe(
      take(1),
      map(loginState => {
        return loginState.user;
      }),
      map(user => {
        const isAuth = !!user;
        if (isAuth) {
          return true;
        }

        return this.router.createUrlTree(['/admin/auth']);
      })
    );
    }

}
