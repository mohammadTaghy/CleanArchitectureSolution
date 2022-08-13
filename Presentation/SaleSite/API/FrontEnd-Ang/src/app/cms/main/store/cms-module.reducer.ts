import { Action } from "rxjs/internal/scheduler/Action";
import { last } from "rxjs/operators";
import { User } from "../../../model/user.model";
import { CurrentState } from "../../common/constant/constant.common";
import * as CmsActions from './cms-module.action'


export interface CmsModuleState<T> {
  items: T[];
  selectedData: T,
  selectedId: number,
  currentSatate: CurrentState,
  error: string;
  loading: boolean;
  pageNumber: number,
  pageSize:number
}

export function CmsModuleReducer<T>(state: CmsModuleState<T>, action: CmsActions.CmsActions<T>) {
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
        currentSatate: action.type,
        selectedData: selected
      }
    case CmsActions.Add_Request_Start:
      return {
        ...state,
        error: '',
        loading: true,
        currentSatate: action.type,
        selectedData: null
      };
    case CmsActions.Edit_Request_Start:
      return {
        ...state,
        error: '',
        loading: true,
        currentSatate: action.type,
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
        currentSatate: CurrentState.List
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
        currentSatate: CurrentState.List
      };
    default:
      return state;
  }
}
