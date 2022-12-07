import { Injectable } from "@angular/core";
import { ColumnProperties } from "../cms/common/constant/constant.common";
import { ConstantData } from "../cms/common/constant/Constant_data.common";
import { ComponentType } from "../cms/common/constant/enum.common";
import { Entity } from "./base_entity/Entity";
import { Membership_Role } from "./membership_roles.model";
import { Membership_User } from "./membership_user.model";

export class Membership_UserProfile extends Entity {
  constructor(public id: number,
    public gender: string,
    public postalCode: string,
    public picturePath: string,
    public birthDate: string,
    public educationGrade: string,
    public userDescription: string,
    public firstName: string,
    public lastName: string,
    public fullName: string,
    public nationalCode: string,
     public userName: string,
    public userCode: string,
    public mobileNumber: string,
    public email: string,
    public isMobileNumberConfirmed: boolean,
    public isEmailConfirmed: boolean,
    public isUserConfirm: boolean,
    public managerConfirm: number,
    public deviceId: string) {
    super();

  }
  public userRoles: Membership_Role[];
  //public id: number;
  //public gender: string;
  //public postalCode: string;
  //public picturePath: string;
  //public birthDate: string;
  //public educationGrade: string;
  //public userDescription: string;
  //public firstName: string;
  //public lastName: string;
  //public fullName: string;
  //public nationalCode: string
  //public userName: string;
  //public userCode: string;
  //public mobileNumber: string;
  //public email: string;
  //public isMobileNumberConfirmed: boolean;
  //public isEmailConfirmed: boolean;
  //public isUserConfirm: boolean;
  //public managerConfirm: number;
  //public deviceId: string;

}

@Injectable()
export class UserGrid {
  gridColumns: ColumnProperties[] = [
    new ColumnProperties("id", "کد رایانه", "col s4 m2 l2", ComponentType.Textbox, "number", true, true, false),
    new ColumnProperties("firstName", "نام", "col s4 m2 l2", ComponentType.Textbox, "string", true),
    new ColumnProperties("lastName", "نام خانوادگی", "col s4 m2 l2", ComponentType.Textbox, "string", true),
    new ColumnProperties("fullName", "نام و نام خانوادگی", "col s4 m2 l2", ComponentType.Textbox, "string", false, true, false),
    new ColumnProperties("nationalCode", "کد ملی", "col s4 m2 l2", ComponentType.Textbox, "string"),
    new ColumnProperties("userName", "نام کاربری", "col s4 m2 l2", ComponentType.Textbox, "string"),
    new ColumnProperties("Password", "رمز ورود", "col s4 m2 l2", ComponentType.Textbox, "password", true,false,false,false,false),
    new ColumnProperties("userCode", "کد کاربر", "col s4 m2 l2", ComponentType.Textbox, "string"),
    new ColumnProperties("mobileNumber", "شماره همراه", "col s4 m2 l2", ComponentType.Textbox, "string"),
    new ColumnProperties("email", "ایمیل", "col s4 m2 l2", ComponentType.Textbox, "email"),
    new ColumnProperties("isMobileNumberConfirmed", "تایید شماره همراه", "col s4 m2 l2", ComponentType.Checkbox, "boolean"),
    new ColumnProperties("isEmailConfirmed", "تایید ایمیل", "col s4 m2 l2", ComponentType.Checkbox, "boolean"),
    new ColumnProperties("isUserConfirm", "تایید کاریر", "col s4 m2 l2", ComponentType.Checkbox, "boolean"),
    new ColumnProperties("managerConfirm", "تایید مدیر", "col s4 m2 l2", ComponentType.Checkbox, "boolean"),
    new ColumnProperties("gender", "جنسیت", "col s4 m2 l2", ComponentType.DropDown, "string", true, false, true, false, true, false, true,
      ConstantData.Gender
    ),
    new ColumnProperties("postalCode", "کد پستی", "col s4 m2 l2", ComponentType.Textbox, "number"),
    new ColumnProperties("birthDate", "تاریخ تولد", "col s4 m2 l2", ComponentType.Textbox, "string"),
    new ColumnProperties("educationGrade", "مقطع تحصیلی", "col s4 m2 l2", ComponentType.DropDown, "string"),
  ];
}



