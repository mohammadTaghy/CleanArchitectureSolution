import { Injectable } from "@angular/core";
import { Action } from "@ngrx/store";
import { Membership_Permission } from "../../../../model/membership/membership_permission.model";

export const AdminPanel_StartLoad = '[AdminPanel] Start Load User Permissions';
export const AdminPanel_Loaded = '[AdminPanel] After Load User Permissions';
export const AdminPanel_FAIL = '[AdminPanel] Load User Permissions Fail';

export class AdminPanelLoaded implements Action {
  readonly type = AdminPanel_Loaded;

  constructor(
    public payload: Membership_Permission[]
  ) { }
}
export class AdminPanelStartLoad implements Action {
  readonly type = AdminPanel_StartLoad;

}

export class AdminPanelFail implements Action {
  readonly type = AdminPanel_FAIL;

  constructor(public payload: string) { }
}

export type AdminPanelPermissionActions =
  AdminPanelLoaded |
  AdminPanelStartLoad |
  AdminPanelFail;
