import { Membership_User } from "../../../model/membership_user.model";
import * as LoginActions from './login.action'


export interface LoginState {
  user: Membership_User;
  authError: string;
  loading: boolean;
}
const initialState: LoginState={
  user:null,
  authError: "",
  loading: false
}

export function LoginReducer(state = initialState, action: LoginActions.LoginActions) {
  switch (action.type) {
    case LoginActions.LOGIN_START:
      return {
        ...state,
        authError: '',
        loading: true,
        user: null
      };
    case LoginActions.Login_FAIL:
      return {
        ...state,
        authError: action.payload,
        loading: false,
        user: null
      };
    case LoginActions.CLEAR_ERROR:
      return {
        ...state,
        authError: "",
        loading: false,
        user: null
      };
    case LoginActions.LOGOUT:
      return {
        ...state,
        authError: "",
        loading: false,
        user: null
      };
    case LoginActions.LOGOUT:
      return {
        ...state,
        authError: "",
        loading: false,
        user: null
      };
    case LoginActions.Login_SUCCESS:
      return {
        ...state,
        authError: "",
        loading: false,
        user: action.payload
      };
    default:
      return state;
  }
}
