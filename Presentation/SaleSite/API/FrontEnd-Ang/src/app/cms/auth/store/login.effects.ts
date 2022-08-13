import { Injectable } from "@angular/core";
import { Actions, ofType, Effect, createEffect, EffectSources } from "@ngrx/effects";
import { Router } from '@angular/router';
import { switchMap, catchError, map, tap } from 'rxjs/operators';
import { of } from "rxjs";

import * as LoginActions from "./login.action";
import { CallAPIComponent } from "../../../commonComponent/callAPI/callAPI.common";
import { ApiAddresses, ApiUrlPostfix } from "../../../commonComponent/apiAddresses/apiAddresses.common";
import { User } from "../../../model/user.model";

class LoginRequestClass {
  constructor(public UserName: string, public Password: string) { }
}

@Injectable()
export class LoginEffects {
  @Effect()
  login$ = this.actions$.pipe(
    ofType(LoginActions.LOGIN_START),
    //map(() => {
    //  console.log('عدم دسترسی');
    //    return { type: 'عدم دسترسی' };
    //})
      switchMap((data: LoginActions.LoginStart) => {
        console.log('effect');
        console.log(data);
        return this.callAPIComponent.PostApi<LoginRequestClass, User>
          (this.apiAddresses.GetServiceUrl(ApiUrlPostfix.CmsLogin),
            new LoginRequestClass(data.payload.userName, data.payload.password))
          .pipe(
            map(resData => {
              localStorage.setItem('userData', JSON.stringify(resData));
              return new LoginActions.LoginSuccess(resData);
            }),
            catchError((errorRes: any) => {
              return of(new LoginActions.LoginFail(errorRes.error));
            }
            ))

      })
    );
  //@Effect({ dispatch: false })
  //authRedirect = this.actions$.pipe(
  //  ofType(LoginActions.Login_SUCCESS),
  //  tap(() => {
  //    this.router.navigate(['/AdminPanel']);
  //  })
  //);
  //@Effect()
  //autoLogin = this.actions$.pipe(
  //  ofType(LoginActions.AUTO_LOGIN),
  //  map(() => {
  //    const user: User = JSON.parse(localStorage.getItem('userData'));
  //    if (!user) {
  //      return { type: 'عدم دسترسی' };
  //    }
  //    return new LoginActions.LoginSuccess(user);
  //  })
  //);
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
    private apiAddresses: ApiAddresses
  ) {
     /*sources.addEffects(this);*/
    console.log('effect inject');
  }

}
