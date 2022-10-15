import { Action } from "rxjs/internal/scheduler/Action";
import { last } from "rxjs/operators";
import { Membership_User } from "../../../model/membership_user.model";
import { CurrentState } from "../../common/constant/constant.common";
import * as CmsActions from './cms-module.action'


export interface CmsModuleState<T> {
  items: T[];
  selectedData: T,
  selectedId: number,
  currentSatate: string,
  error: string;
  loading: boolean;
  pageNumber: number,
  pageSize:number
}

export function CmsModuleReducer<T>(state: CmsModuleState<T>, action: CmsActions.CmsActions) {
  switch (action.type) {
    case CmsActions.Changed_View:
      let selected:T = null;
      if (action.viewType == CurrentState.Delete || action.viewType == CurrentState.Edit ||
        action.viewType == CurrentState.Details)
        selected = state.items.filter(p => p["Id"] == action.selectedId)[0];
      return {
        ...state,
        error: '',
        loading: false,
        currentSatate: action.type.toString(),
        selectedData: selected
      }
    case CmsActions.Add_Request_Start:
      return {
        ...state,
        error: '',
        loading: true,
        currentSatate: action.type.toString(),
        selectedData: null
      };
    case CmsActions.Edit_Request_Start:
      return {
        ...state,
        error: '',
        loading: true,
        currentSatate: action.type.toString(),
        selectedData: state.items.find(p => p["Id"] == action.payload["Id"])
      };
    case CmsActions.Delete_Request_Start:
      return {
        ...state,
        error: "",
        loading: true,
        selectedId: action.payload
      };
    case CmsActions.Request_SUCCESS:

      return {
        ...state,
        error: "",
        loading: false,
        selectedData: null,
        currentSatate: CurrentState.List.toString()
      };
    
    case CmsActions.Request_FAIL:
      return {
        ...state,
        error: action.payload,
        loading: false,
      };
    case CmsActions.CLEAR_ERROR:
      return {
        ...state,
        error: "",
        loading: false,
      };
    case CmsActions.Set_Data:
      return {
        ...state,
        error: "",
        loading: false,
        items: action.payload,
        selectedId: null,
        currentSatate: CurrentState.List.toString()
      };
    default:
      return state;
  }
}
