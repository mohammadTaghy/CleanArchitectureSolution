import { EnvironmentInjector, Injectable } from "@angular/core";


export enum ApiUrlPostfix {
  CmsLogin = "user/SignIn",
  MembershipUsers = "User/Users",
  Permissions ="Permission/Permissions",
  CurrentUserPermissions ="Permission/CurrentUserPermissions",
  LessonsCategoriesAsList = "LessonsCategories/LessonsCategoriesAsList",
  LessonsCategories = "LessonsCategories/LessonsCategories",
  FileUploadAddress = "/"
};
@Injectable({
  providedIn: 'root'
})
export class ApiAddresses {
  constructor() { }
  public baseUrl: string = "https://localhost:44376/api/";
  GetServiceUrl(urlPostfix: string): string {
    return (this.baseUrl + urlPostfix).trim();
    //return "";
  }
}
