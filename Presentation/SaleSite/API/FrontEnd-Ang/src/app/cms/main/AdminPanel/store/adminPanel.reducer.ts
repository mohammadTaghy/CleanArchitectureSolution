import { Membership_Permission } from "../../../../model/membership_permission.model";
import * as fromAdminPanelActions from './adminPanel.action'


export interface AdminPanelState {
  permissions: Membership_Permission[];
  authError: string;
  loading: boolean;
}
const initialState: AdminPanelState={
  permissions:null,
  authError: "",
  loading: false
}

export function AdminPanelReducer(state = initialState, action: fromAdminPanelActions.AdminPanelPermissionActions) {
  switch (action.type) {
    case fromAdminPanelActions.AdminPanel_StartLoad:
      return {
        ...state,
        authError: '',
        loading: true,
        permissions: null
      };
    case fromAdminPanelActions.AdminPanel_Loaded:
      return {
        ...state,
        authError: '',
        loading: false,
        permissions: action.payload
      };
    case fromAdminPanelActions.AdminPanel_FAIL:
      return {
        ...state,
        authError: action.payload,
        loading: false,
        permissions: null
      };
    default:
      return state;
  }
}
