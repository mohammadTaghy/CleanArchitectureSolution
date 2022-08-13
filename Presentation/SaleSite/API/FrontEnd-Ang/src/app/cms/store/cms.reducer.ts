
import { ActionReducerMap } from '@ngrx/store'
import * as FromCmsLogin from '../auth/store/login.reducer'
import * as FromCmsModule from '../main/store/cms-module.reducer'

export interface CmsState {
  loginState: FromCmsLogin.LoginState
}

export const cmsReducer: ActionReducerMap<CmsState> = {
  loginState: FromCmsLogin.LoginReducer
}
