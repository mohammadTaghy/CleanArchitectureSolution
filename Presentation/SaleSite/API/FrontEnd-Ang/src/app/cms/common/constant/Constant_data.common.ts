import { DropdownDataType } from "./constant.common";

export class ConstantData {
  public static readonly Gender = [
    new DropdownDataType("مرد", "جنسیت خود را انتخاب کنید", 0, false),
    new DropdownDataType("زن", "جنسیت خود را انتخاب کنید", 1, false)
  ]
}
