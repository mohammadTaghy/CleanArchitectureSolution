import { Injectable } from "@angular/core";
import { Actions, ofType, Effect } from "@ngrx/effects";
import { Router } from '@angular/router';
import { switchMap, catchError, map, tap } from 'rxjs/operators';
import { HttpClient } from "@angular/common/http";
import { Store } from "@ngrx/store";
import { of } from "rxjs";

import * as CmsActions from "./cms-module.action";
import { CallAPIComponent } from "../../../commonComponent/callAPI/callAPI.common";
import { ApiAddresses, ApiUrlPostfix } from "../../../commonComponent/apiAddresses/apiAddresses.common";
import { FilterRequestBody, QueryRequestBody } from "../../common/filterRequestBody/filter-request-body.common";

class CmsResponce<TResponse> {
  constructor(public errors: string, public isSuccess: boolean, public result: TResponse) { }
}
function SendRequestProcess<TRequest, TRespnse>(url: string, payLoad, callAPIComponent: CallAPIComponent) {
  return callAPIComponent.PostApi<TRequest, TRespnse>(url, payLoad)
    .pipe(
      map(resData => {
        return new CmsActions.RequestSuccess<TRespnse>(resData);
      }),
      catchError((errorRes: any) => {
        return of(new CmsActions.RequestFail(errorRes.error));
      }
      ))
}

@Injectable({ providedIn: 'root' })
export class CmsModuleEffects<TRequest, TRespnse> {
  constructor(private actions$: Actions, private router: Router, private http: HttpClient,
    private callAPIComponent: CallAPIComponent, private apiAddresses: ApiAddresses) { }
  @Effect()
  addRequestStart = this.actions$.pipe(
    ofType(CmsActions.Add_Request_Start),
    switchMap((data: CmsActions.AddRequestStart<TRequest>) => {
      return SendRequestProcess<TRequest, CmsResponce<TRespnse>>(data.serviceUrl, data.payload, this.callAPIComponent);
    })
  )
  @Effect()
  editRequestStart = this.actions$.pipe(
    ofType(CmsActions.Edit_Request_Start),
    switchMap((data: CmsActions.EditRequestStart<TRequest>) => {
      return SendRequestProcess<TRequest, CmsResponce<TRespnse>>(data.serviceUrl, data.payload, this.callAPIComponent);
    })
  )
  @Effect()
  deleteRequestStart = this.actions$.pipe(
    ofType(CmsActions.Delete_Request_Start),
    switchMap((data: CmsActions.DeleteRequestStart<TRequest>) => {
      return SendRequestProcess<TRequest, CmsResponce<TRespnse>>(data.serviceUrl, data.payload, this.callAPIComponent);
    })
  )
  
  @Effect()
  fetchData = this.actions$.pipe(
    ofType(CmsActions.Fetch_Data),
    switchMap((data: CmsActions.FetchData) => {
      //console.log("fetch data effect");
      //console.log(data.payload);
      return this.callAPIComponent.GetApi<CmsResponce<TRespnse[]>>(data.serviceUrl, data.payload)
    }),
    map(resData => {
      //console.log("resData");
      //console.log(resData);
      return new CmsActions.SetData<TRespnse>(resData.result)
    })
  );
}
