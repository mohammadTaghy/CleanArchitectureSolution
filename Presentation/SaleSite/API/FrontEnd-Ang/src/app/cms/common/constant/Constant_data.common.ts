import { DropdownDataType } from "./constant.common";
import * as enumConstant from "./enum.common";

export class ConstantData {
  public static readonly Gender = [
    new DropdownDataType("مرد", "جنسیت خود را انتخاب کنید", 0, false),
    new DropdownDataType("زن", "جنسیت خود را انتخاب کنید", 1, false)
  ];
  public static readonly LessonsCategories = [
    new DropdownDataType("مقطع تحصیلی", "مانند:دوره ابتدایی", 0, false),
    new DropdownDataType("پایه تحصیلی", "مانند:پایه اول", 1, false),
    new DropdownDataType("کتاب درسی", "مانند:کتاب فارسی", 2, false),
    new DropdownDataType("بخش", "مانند:بخش اول", 3, false),
    new DropdownDataType("درس", "مانند:درس اول", 4, false),
  ];
  public static readonly FeatureType = [
    new DropdownDataType("منو", "در منو پنل مشاهده می شود", 0, false),
    new DropdownDataType("فرم", "یک فرم است", 1, false),
    new DropdownDataType("تب", "تب های داخل فرم", 2, false),
    new DropdownDataType("کامند", "اکشن های برنامه", 3, false),
  ];
  public static readonly FilterType = [
    { title: "شامل", value: enumConstant.FilterType.Like, description: "like '%__%'", disabled: false },
    { title: "شروع شود با", value: enumConstant.FilterType.StartWith, description: "like '__%'", disabled: false },
    { title: "خاتمه یابد با", value: enumConstant.FilterType.EndWith, description: "like '%__'", disabled: false },
    { title: "برابر", value: enumConstant.FilterType.Equal, description: "==", disabled: false },
    { title: "بزرگتر از", value: enumConstant.FilterType.GreaterThan, description: ">", disabled: false },
    { title: "کوچکتر از", value: enumConstant.FilterType.LessThan, description: "<", disabled: false },
    { title: "بزرگتر و مساوی از", value: enumConstant.FilterType.GreaterOrEqual, description: ">=", disabled: false },
    { title: "کوچکتر و مساوی از", value: enumConstant.FilterType.LessOrEqual, description: "<=", disabled: false },
  ];
}

