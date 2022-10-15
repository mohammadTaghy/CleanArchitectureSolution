import { Injectable } from "@angular/core";

export enum ApiUrlPostfix {
  CmsLogin = "user/SignIn",
  AdminPanelPermission = "permission/GetCurrentUserPermissions",
  MembershipUsers="User/GetAll"
};
@Injectable({
  providedIn: 'root'
})
export class ApiAddresses {
  baseUrl: string = "http://localhost:18023/api/";
  GetServiceUrl(urlPostfix: string): string {
    return this.baseUrl + urlPostfix;
    //return "";
  }
}
