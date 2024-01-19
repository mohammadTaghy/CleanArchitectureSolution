import { ComponentType, JoinCondition, SortType } from "./enum.common";

export class ConstantNameString {
  public static Token: string = "Token";
}

export class ColumnProperties {
  constructor(
    public field: string,
    public header: string,
    public width: string = "w-50",
    public componentType: ComponentType,
    public type: string,
    public isRequired: boolean=true,
    public isReadonly: boolean = false,
    public isDialogVisible: boolean = true,
    public isSortable: boolean = true,
    public isGridVisible: boolean = true,
    public isPinned: boolean = false,
    public isPassword: boolean = false,
    public dataSource: DropdownDataType[] = null,
    public ApiName: string="",
    public baseFilter: string="",
    public effectColumn: string = "",
    public uploadUrl: string = "",
    public isMultiFile: boolean = false,
    public fileTypeCanChoose: string=""
  ) { }

}
export class DropdownDataType {
  constructor(
  public title: string,
    public description: string,
    public value: string | number,
    public disabled: boolean,) { }
}
export interface IFilterData {
  selectedColumn: string | number;
  selectedFilterType: number;
  value: string;
  name: string;
  joinCondition: JoinCondition
}
export interface ISortData {
  selectedColumn: string | number;
  sortType: SortType;
  name: string;
}



