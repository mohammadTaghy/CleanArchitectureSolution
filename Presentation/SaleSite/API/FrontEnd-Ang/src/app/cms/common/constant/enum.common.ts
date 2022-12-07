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
  DatePicker
}
export enum FilterType {
  Like,
  EndWith,
  StartWith,
  Equal,
  GreaterThan,
  LessThan,
  GreaterOrEqual,
  LessOrEqual
}
export enum SortType {
  Ascending,
  Descending
}
export enum JoinCondition {
  And,
  Or
}
