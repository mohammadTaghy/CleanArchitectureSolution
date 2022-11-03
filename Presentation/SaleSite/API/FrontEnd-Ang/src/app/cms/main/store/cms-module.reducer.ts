import { Action } from "rxjs/internal/scheduler/Action";
import { last } from "rxjs/operators";
import { Membership_User } from "../../../model/membership_user.model";
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

const initialState: CmsModuleState<any> = {
  error: "",
  loading: true,
  items: null,
  selectedId: null,
  currentSatate: CurrentState.List,
  pageNumber: 1,
  pageSize: 10,
  selectedData:null
}

export function CmsModuleReducer<T>(state = initialState, action: CmsActions.CmsActions) {
  switch (action.type) { 
    case CmsActions.Changed_View:
      let selected:T = null;
      if (action.viewType == CurrentState.Delete || action.viewType == CurrentState.Edit ||
        action.viewType == CurrentState.Details)
        selected = state.items.filter(p => p["id"] == action.selectedId)[0];
      return {
        ...state,
        error: '',
        loading: false,
        currentSatate: action.viewType,
        selectedData: selected
      }
    case CmsActions.Add_Request_Start:
      return {
        ...state,
        error: '',
        loading: true,
        selectedData: null
      };
    case CmsActions.Edit_Request_Start:
      return {
        ...state,
        error: '',
        loading: true,
        selectedData: state.items.find(p => p["id"] == action.payload["id"])
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
      //console.log("setData");
      //console.log(action.payload);
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
