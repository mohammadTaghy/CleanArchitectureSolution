import { Injectable } from "@angular/core";
import { Actions, ofType, Effect } from "@ngrx/effects";
import { Router } from '@angular/router';
import { switchMap, catchError, map, tap } from 'rxjs/operators';
import { HttpClient } from "@angular/common/http";
import { Store } from "@ngrx/store";
import { of } from "rxjs";

import * as CmsActions from "./cms-module.action";
import * as constant from "../../common/constant/constant.common";

import { CallAPIComponent } from "../../../commonComponent/callAPI/callAPI.common";
import { ApiAddresses, ApiUrlPostfix } from "../../../commonComponent/apiAddresses/apiAddresses.common";
import { FilterRequestBody, QueryRequestBody } from "../../common/filterRequestBody/filter-request-body.common";
import { cmsReducer } from "../../store/cms.reducer";

class CmsResponce<TResponse> {
  constructor(public errors: string, public isSuccess: boolean, public result: TResponse) { }
}

@Injectable({ providedIn: 'root' })
export class CmsModuleEffects<TRequest, TRespnse> {
  
  constructor(private actions$: Actions, private router: Router, private http: HttpClient,
    private callAPIComponent: CallAPIComponent, private apiAddresses: ApiAddresses) { }
  @Effect()
  addRequestStart = this.actions$.pipe(
    ofType(CmsActions.Add_Request_Start),
    switchMap((data: CmsActions.AddRequestStart<TRequest>) => {
      console.log(data);
      return this.callAPIComponent.postApi<TRequest, TRespnse>(data.serviceUrl, data.payload)
        .pipe(
          map(resData => {
            return new CmsActions.RequestSuccess<TRespnse>(resData);
          }),
          catchError((errorRes: any) => {
            return of(new CmsActions.RequestFail(errorRes.error));
          }
          ))
    })
  )
  @Effect()
  editRequestStart = this.actions$.pipe(
    ofType(CmsActions.Edit_Request_Start),
    switchMap((data: CmsActions.EditRequestStart<TRequest>) => {
      return this.callAPIComponent.putApi<TRequest, TRespnse>(data.serviceUrl, data.payload)
        .pipe(
          map(resData => {
            return new CmsActions.RequestSuccess<TRespnse>(resData);
          }),
          catchError((errorRes: any) => {
            return of(new CmsActions.RequestFail(errorRes.error));
          }
          ));

    })
  )
  @Effect()
  deleteRequestStart = this.actions$.pipe(
    ofType(CmsActions.Delete_Request_Start),
    switchMap((data: CmsActions.DeleteRequestStart<TRequest>) => {
      return this.callAPIComponent.deleteApi<TRequest, TRespnse>(data.serviceUrl)
        .pipe(
          map(resData => {
            return new CmsActions.RequestSuccess<TRespnse>(resData);
          }),
          catchError((errorRes: any) => {
            return of(new CmsActions.RequestFail(errorRes.error));
          }
          ))
    })
  )

  @Effect()
  fetchData = this.actions$.pipe(
    ofType(CmsActions.Fetch_Data),
    switchMap((data: CmsActions.FetchData) => {
      let url: string = data.serviceUrl;
      console.log(url);
      url += this.addFilterToUrl(data.filter);
      console.log(url);
      url += this.addOrderbyToUrl(data.sort);
      console.log(url);
      return this.callAPIComponent.GetApi<CmsResponce<TRespnse[]>>(url);
    }),
    map(resData => {
      //console.log("resData");
      //console.log(resData);
      return new CmsActions.SetData<TRespnse>(resData.result)
    })
  );
  addFilterToUrl(filter: constant.IFilterData[]) {
    let changeToFilterStaring: string = "",
      sign:string="";

    if (filter != null && filter.length > 0) {
      filter.filter(p => p.joinCondition == constant.JoinCondition.And).forEach(p => {
        changeToFilterStaring += this.makeFilter(p.selectedFilterType, sign, p.value, p.selectedColumn.toString());
        sign = " and ";
      });
      sign+=" ("
      filter.filter(p => p.joinCondition == constant.JoinCondition.Or).forEach(p => {
        changeToFilterStaring += this.makeFilter(p.selectedFilterType, sign, p.value, p.selectedColumn.toString());
        sign = " or ";
      });
      if (sign == " or ")
        changeToFilterStaring += ")";
      changeToFilterStaring = "&Filter=" + changeToFilterStaring;
    }
    return changeToFilterStaring;
  }
  addOrderbyToUrl(sort: constant.ISortData[]) {
    let changeToOrderbyStaring: string = " ", sign: string = "";
    if (sort != undefined&& sort != null && sort.length > 0) {
      sort.forEach(p => {
        changeToOrderbyStaring += sign + p.selectedColumn + " " + (p.sortType == constant.SortType.Ascending ? "asc" : "desc");
        sign = " ,"
      });
      changeToOrderbyStaring = "&Orderby=" + changeToOrderbyStaring;
    }
    return changeToOrderbyStaring;
  }
  makeFilter(selectedFilterType: constant.FilterType, sign, value: string, selectedColumn: string) {
    switch (selectedFilterType) {
      case constant.FilterType.Like:
        return sign + " " + selectedColumn + " like N'%" + value + "%'";
        
      case constant.FilterType.StartWith:
       return sign + " " + selectedColumn + " like N'" + value + "%'";
        
      case constant.FilterType.EndWith:
       return sign + " " + selectedColumn + " like N'%" + value + "'";
        
      case constant.FilterType.GreaterOrEqual:
       return sign + " " + selectedColumn + " >= " + value;
        
      case constant.FilterType.LessOrEqual:
       return sign + " " + selectedColumn + " =< " + value;
        
      case constant.FilterType.GreaterThan:
       return sign + " " + selectedColumn + " > " + value;
        
      case constant.FilterType.LessThan:
       return sign + " " + selectedColumn + " < " + value;
        
      default:
       return sign + " " + selectedColumn + " = " + value;
        
    }
  }
}


