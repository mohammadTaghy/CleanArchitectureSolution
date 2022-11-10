import { Injectable } from "@angular/core";
import { ColumnProperties, ComponentType, dropdownDataType } from "../cms/common/constant/constant.common";
import { Entity } from "./base_entity/Entity";
import { Membership_Role } from "./membership_roles.model";
import { Membership_User } from "./membership_user.model";

export class Membership_UserProfile extends Entity {
  public userRoles: Membership_Role[];
  public id: number;
  public gender: string;
  public postalCode: string;
  public picturePath: string;
  public birthDate: string;
  public educationGrade: string;
  public userDescription: string;
  public firstName: string;
  public lastName: string;
  public fullName: string;
  public nationalCode: string
  public userName: string;
  public userCode: string;
  public mobileNumber: string;
  public email: string;
  public isMobileNumberConfirmed: boolean;
  public isEmailConfirmed: boolean;
  public isUserConfirm: boolean;
  public managerConfirm: number;
  public deviceId: string;

}

@Injectable()
export class UserGrid {
  gridColumns: ColumnProperties[] = [
    new ColumnProperties("id", "کد رایانه", "W-100", ComponentType.Textbox, "number", true, true, false),
    new ColumnProperties("firstName", "نام", "w-200", ComponentType.Textbox, "string", true),
    new ColumnProperties("lastName", "نام خانوادگی", "w-200", ComponentType.Textbox, "string", true),
    new ColumnProperties("fullName", "نام و نام خانوادگی", "w-200", ComponentType.Textbox, "string", false, true, false),
    new ColumnProperties("nationalCode", "کد ملی", "100px", ComponentType.Textbox, "string"),
    new ColumnProperties("userName", "نام کاربری", "50px", ComponentType.Textbox, "string"),
    new ColumnProperties("userCode", "کد کاربر", "200px", ComponentType.Textbox, "string"),
    new ColumnProperties("mobileNumber", "شماره همراه", "50px", ComponentType.Textbox, "string"),
    new ColumnProperties("email", "ایمیل", "50px", ComponentType.Textbox, "email"),
    new ColumnProperties("isMobileNumberConfirmed", "تایید شماره همراه", "50px", ComponentType.Checkbox, "boolean"),
    new ColumnProperties("isEmailConfirmed", "تایید ایمیل", "50px", ComponentType.Checkbox, "boolean"),
    new ColumnProperties("isUserConfirm", "تایید کاریر", "50px", ComponentType.Checkbox, "boolean"),
    new ColumnProperties("managerConfirm", "تایید مدیر", "50px", ComponentType.Checkbox, "boolean"),
    new ColumnProperties("gender", "جنسیت", "50px", ComponentType.DropDown, "string", true, false, true, false, true, false, true,
       [
        new dropdownDataType("مرد", "جنسیت خود را انتخاب کنید", 0, false),
        new dropdownDataType("زن", "جنسیت خود را انتخاب کنید", 1, false)
    ]
    ),
    new ColumnProperties("postalCode", "کد پستی", "50px", ComponentType.Textbox, "number"),
    new ColumnProperties("birthDate", "تاریخ تولد", "50px", ComponentType.DatePicker, "string"),
    new ColumnProperties("educationGrade", "مقطع تحصیلی", "50px", ComponentType.DropDown, "string"),
  ];
}

