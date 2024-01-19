import { Injectable } from "@angular/core";
import { ColumnProperties } from "../../cms/common/constant/constant.common";
import { ConstantData } from "../../cms/common/constant/Constant_data.common";
import { ComponentType } from "../../cms/common/constant/enum.common";
import { HierarchyEntity } from "../base_entity/HierarchyEntity";

export class Common_LessonsCategories extends HierarchyEntity<Common_LessonsCategories>{
  constructor(public name: string,
    public title: string,
    public isActive: boolean,
    public iConPath: string,
    public coefficient: number,
    public categoryType: number) {
    super();
  }
  
}
@Injectable()
export class LessonsCategoriesGrid {
  gridColumns: ColumnProperties[] = [
    new ColumnProperties("id", "کد رایانه", "col s4 m2 l2", ComponentType.Textbox, "number", true, true, false),
    new ColumnProperties("name", "نام", "col s4 m2 l2", ComponentType.Textbox, "string", true),
    new ColumnProperties("title", "عنوان", "col s4 m2 l2", ComponentType.Textbox, "string", true),
    new ColumnProperties("isActive", "فعال", "col s4 m2 l2", ComponentType.Checkbox, "boolean"),
    new ColumnProperties("Coefficient", "تایید ایمیل", "col s4 m2 l2", ComponentType.Checkbox, "boolean"),
    new ColumnProperties("CategoryType", "نوع مقطع", "col s4 m2 l2", ComponentType.DropDown, "string", true, false, true, false, true, false, true,
      ConstantData.LessonsCategories
    ),
    new ColumnProperties("IConPath", "ادرس آیکن", "col s4 m2 l2", ComponentType.UploadFile, "number"),
  ];
}
