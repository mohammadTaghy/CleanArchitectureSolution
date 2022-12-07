import { Injectable } from "@angular/core";
import { Actions, ofType, Effect, createEffect, EffectSources } from "@ngrx/effects";
import { Router } from '@angular/router';
import { switchMap, catchError, map, tap } from 'rxjs/operators';
import { observable, of } from "rxjs";

import * as adminPanelActions from "./adminPanel.action";
import { CallAPIComponent } from "../../../../commonComponent/callAPI/callAPI.common";
import { ApiAddresses, ApiUrlPostfix } from "../../../../commonComponent/apiAddresses/apiAddresses.common";
import { Membership_Permission } from "../../../../model/membership_permission.model";
import { HttpHeaderResponse } from "@angular/common/http";
import { QueryResponse } from "../../../../commonComponent/common_model/query_response";



@Injectable()
export class AdminPanelEffects {
  @Effect()
  adminPanleStartLoad$ = this.actions$.pipe(
    ofType(adminPanelActions.AdminPanel_StartLoad),
    switchMap((data: adminPanelActions.AdminPanelStartLoad) => {
      //console.log("get adminpanel");
      return this.callAPIComponent.GetApi<QueryResponse<Membership_Permission[]>>
        (this.apiAddresses.GetServiceUrl(ApiUrlPostfix.AdminPanelPermission))
        .pipe(
          map(resData => {
            //console.log(resData);
            return new adminPanelActions.AdminPanelLoaded(resData.result);
          }),
          catchError((errorRes: any) => {
            return of(new adminPanelActions.AdminPanelFail(errorRes.error));
          }
          ))

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
