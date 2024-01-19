import { Injectable } from "@angular/core";
import { Actions, ofType, Effect, createEffect, EffectSources } from "@ngrx/effects";
import { Router } from '@angular/router';
import { switchMap, catchError, map, tap } from 'rxjs/operators';
import { observable, of } from "rxjs";

import * as LoginActions from "./login.action";
import { CallAPIComponent } from "../../../commonComponent/callAPI/callAPI.common";
import { ApiAddresses, ApiUrlPostfix } from "../../../commonComponent/apiAddresses/apiAddresses.common";
import { Membership_User } from "../../../model/membership/membership_user.model";
import { HttpHeaderResponse } from "@angular/common/http";

class LoginRequestClass {
  constructor(public UserName: string, public Password: string) { }
}

@Injectable()
export class LoginEffects {
  @Effect()
  login$ = this.actions$.pipe(
    ofType(LoginActions.LOGIN_START),
    switchMap((data: LoginActions.LoginStart) => {
      return this.callAPIComponent.postApi<LoginRequestClass, Membership_User>
        (this.apiAddresses.GetServiceUrl(ApiUrlPostfix.CmsLogin), new LoginRequestClass(data.payload.userName, data.payload.password))
        .pipe(
          map(resData => {
            var user = new Membership_User();
            console.log("login result");
            localStorage.setItem('userData', JSON.stringify(resData));
            return new LoginActions.LoginSuccess(resData);
          }),
          catchError((errorRes: any) => {
           console.log(errorRes);
            return of(new LoginActions.LoginFail(errorRes.error));
          }
          ))

    })
  );
  @Effect({ dispatch: false })
  authRedirect = this.actions$.pipe(
    ofType(LoginActions.Login_SUCCESS),
    tap(() => {
     //console.log("successredirect");
      if (window.location.pathname != "/admin") {
       //console.log("successredirect1");
        this.router.navigate(['/']);
      }
    })
  );
  @Effect({ dispatch: false })
  failRedirect = this.actions$.pipe(
    ofType(LoginActions.Login_FAIL),
    tap(() => {
     //console.log("failredirect");
      if (window.location.pathname != "/admin/auth") {
        location.href = "http://localhost:18023/admin/auth";
      }
    })
  );
  @Effect()
  autoLogin = this.actions$.pipe(
    ofType(LoginActions.AUTO_LOGIN),
    map(() => {
     //console.log("AUTO_LOGIN");
      const user: Membership_User = JSON.parse(localStorage.getItem('userData'));
     //console.log(user);
     //console.log('user');
      if (user == null) {
       //console.log('redirect');
        return new LoginActions.LoginFail("حق دسترسی ندارید");
      }

    })
  );
  //@Effect()
  //logout = this.actions$.pipe(
  //  ofType(LoginActions.LOGOUT),
  //  tap(() => {
  //    new LoginActions.Logout();
  //    localStorage.removeItem('userData')
  //    this.router.navigate([''])
  //  })
  //)
  constructor(
    private sources: EffectSources,
    private actions$: Actions,
    private callAPIComponent: CallAPIComponent,
    private apiAddresses: ApiAddresses,
    private router: Router
  ) {
    /*sources.addEffects(this);*/
  }

}
