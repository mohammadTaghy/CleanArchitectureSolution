
import { ActionReducerMap } from '@ngrx/store'
import * as fromCmsLogin from '../auth/store/login.reducer'
import * as fromCmsReducer from '../main/store/cms-module.reducer'
import * as fromAdminPanelReducer from '../main/AdminPanel/store/adminPanel.reducer'

import * as CmsActions from '../main/store/cms-module.action'
import { Action } from 'rxjs/internal/scheduler/Action'

export interface CmsState<T> {
  loginState: fromCmsLogin.LoginState,
  adminPanelState: fromAdminPanelReducer.AdminPanelState,
  moduleState: fromCmsReducer.CmsModuleState<T>
}

export const cmsReducer: ActionReducerMap<CmsState<any>> = {
  loginState: fromCmsLogin.LoginReducer,
  adminPanelState: fromAdminPanelReducer.AdminPanelReducer,
  moduleState: fromCmsReducer.CmsModuleReducer<any>
}
