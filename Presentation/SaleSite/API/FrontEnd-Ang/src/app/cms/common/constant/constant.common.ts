export enum CurrentState {
  Insert,
  Edit,
  Delete,
  Details,
  List
}

export enum FeatureType {
  Menu = 0,
  Form,
  Tab,
  Command
}

export enum ComponentType {
  Textbox = 0,
  Checkbox,
  RadioButton,
  DropDown,
  UploadFile,
  Button,
  List,
  Tree,
  HtmlEditor,
}

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
    public dataSource: dropdownDataType[] = null

  ) { }

}
export class dropdownDataType {
  constructor(
  public title: string,
  public description: string,
  public value: number | string,
    public disabled: boolean,) { }
}


