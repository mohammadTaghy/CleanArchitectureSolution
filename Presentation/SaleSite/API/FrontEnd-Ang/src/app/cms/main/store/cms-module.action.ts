import { Action } from "@ngrx/store";
import { CurrentState, IFilterData, ISortData } from "../../common/constant/constant.common";
import { FilterRequestBody, QueryRequestBody } from "../../common/filterRequestBody/filter-request-body.common";

export const Add_Request_Start = '[Module] Add Request Start';
export const Edit_Request_Start = '[Module] Edit Request Start';
export const Delete_Request_Start = '[Module] Delete Request Start';
export const Request_SUCCESS = '[Module] Success';
export const Request_FAIL = '[Module] Request Fail';
export const CLEAR_ERROR = '[Module] Clear Error';
export const Set_Data = '[Module] Set Data';
export const Fetch_Data = '[Module] Fetch Data';
export const Changed_View ='[Module] Changed View'



export class RequestSuccess<T> implements Action {
  readonly type = Request_SUCCESS;

  constructor(
    public payload: T
  ) { }
}

export class AddRequestStart<T> implements Action {
  readonly type = Add_Request_Start;

  constructor(public payload: T, public serviceUrl: string) { }
}

export class EditRequestStart<T> implements Action {
  readonly type = Edit_Request_Start;

  constructor(public payload: T, public serviceUrl: string) { }
}

export class DeleteRequestStart<T> implements Action {
  readonly type = Delete_Request_Start;

  constructor(public serviceUrl: string) { }
}

export class RequestFail implements Action {
  readonly type = Request_FAIL;

  constructor(public payload: string) { }
}

export class FetchData implements Action {
  readonly type = Fetch_Data;

  constructor(public serviceUrl: string, public filter: IFilterData[], public sort: ISortData[]) { }
}

export class SetData<T> implements Action {
  readonly type = Set_Data;

  constructor(
    public payload: T[]
  ) { }
}

export class ClearError implements Action {
  readonly type = CLEAR_ERROR;
}
export class ChangedView implements Action {
  readonly type = Changed_View;
  constructor(public viewType: CurrentState, public selectedId:number) { }
}


export type CmsActions =
  RequestSuccess<any>
  | AddRequestStart<any>
  | EditRequestStart<any>
  | DeleteRequestStart<any>
  | RequestFail
  | ClearError
  | FetchData
  | SetData<any>
  | ChangedView
  ;
