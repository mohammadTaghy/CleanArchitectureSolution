import { Injectable } from "@angular/core";
import { Action } from "@ngrx/store";
import { User } from "../../../model/user.model";

export const LOGIN_START = '[User] Login Start';
export const Login_SUCCESS = '[User] Login';
export const Login_FAIL = '[User] Login Fail';
export const CLEAR_ERROR = '[User] Clear Error';
export const AUTO_LOGIN = '[User] Auto Login';
export const LOGOUT = '[User] Logout';


export class LoginSuccess implements Action {
  readonly type = Login_SUCCESS;

  constructor(
    public payload: User
  ) { }
}

export class Logout implements Action {
  readonly type = LOGOUT;
}
export class LoginStart implements Action {
  readonly type = LOGIN_START;

  constructor(public payload: { userName: string; password: string }) {  }
}

export class LoginFail implements Action {
  readonly type = Login_FAIL;

  constructor(public payload: string) { }
}


export class ClearError implements Action {
  readonly type = CLEAR_ERROR;
}

export class AutoLogin implements Action {
  readonly type = AUTO_LOGIN;
}


export type LoginActions =
  LoginStart
  | LoginSuccess
  | Logout
  | LoginFail
  | ClearError
  | AutoLogin
  ;
