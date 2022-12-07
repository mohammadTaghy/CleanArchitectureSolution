import { EnvironmentInjector, Injectable } from "@angular/core";

import { Config } from "../config/config"

export enum ApiUrlPostfix {
  CmsLogin = "user/SignIn",
  AdminPanelPermission = "permission/Permissions",
  MembershipUsers = "User/Users",
};
@Injectable({
  providedIn: 'root'
})
export class ApiAddresses {
  constructor() { }
  baseUrl: string = "http://localhost:18023/api/";
  GetServiceUrl(urlPostfix: string): string {
    return this.baseUrl + urlPostfix + "?api-version=" + Config.version;
    //return "";
  }
}
