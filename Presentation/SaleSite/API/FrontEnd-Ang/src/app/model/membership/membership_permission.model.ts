import { Injectable } from "@angular/core";
import { ColumnProperties } from "../../cms/common/constant/constant.common";
import { ConstantData } from "../../cms/common/constant/Constant_data.common";
import { ComponentType } from "../../cms/common/constant/enum.common";
import { ApiAddresses } from "../../commonComponent/apiAddresses/apiAddresses.common";
import { HierarchyEntity } from "./../base_entity/HierarchyEntity";
//import { ITreeNode } from "../commonComponent/componnent/material_flatTree"

export class Membership_Permission extends HierarchyEntity<Membership_Permission> {
  constructor(public name: string,
    public title: string,
    public commandName: string,
    public featureType: number,
    public isActive: boolean,
    public hasPermission: boolean) {
    super();
  }
  
}
@Injectable()
export class PermissionGrid {
  constructor(private apiAddresses: ApiAddresses) {

  }
  gridColumns: ColumnProperties[] = [
    new ColumnProperties("id", "کد رایانه", "col s4 m2 l2", ComponentType.Textbox, "number", true, true, false),
    new ColumnProperties("name", "نام", "col s4 m2 l2", ComponentType.Textbox, "string", true),
    new ColumnProperties("title", "عنوان", "col s4 m2 l2", ComponentType.Textbox, "string", true),
    new ColumnProperties("commandName", "نام کامند", "col s4 m2 l2", ComponentType.Textbox, "string", true),
    new ColumnProperties("isActive", "فعال", "col s4 m2 l2", ComponentType.Checkbox, "boolean"),
    new ColumnProperties("parentId", "کد پدر", "col s4 m2 l2", ComponentType.Textbox, "number", false, true, false, false, false),
    new ColumnProperties("featureType", "نوع فیچر", "col s4 m2 l2", ComponentType.DropDown, "string", true, false, true, false, true, false, true,
      ConstantData.FeatureType
    ),
    new ColumnProperties("IconPath", "آیکن", "col s4 m2 l2", ComponentType.UploadFile, "string", true, false, true, false, true, false, false,
      [],"", "", "", (this.apiAddresses.baseUrl + "Permission/UploadFile?api-version=1.0"), false,"image/*")
    ,
  ];
}
