import { Component, EventEmitter, Input, OnChanges, OnInit, Output, QueryList, SimpleChanges, ViewChildren } from "@angular/core";
import { Action } from "@ngrx/store";

import { ColumnProperties, CurrentState } from "../../constant/constant.common";
import { BaseUIComponent, IBaseUIComponent } from "../baseUI.compnent";
import * as fromCmsAction from "../../../main/store/cms-module.action"
import { TextboxComponnent } from "../textbox/textbox.component";
import { DropdownComponnent } from "../dropdown/dropdown.component";
import { CheckboxComponnent } from "../checkbox/checkbox.component";
import { DatePickerComponnent } from "../datepicker/datepicker.component";

@Component({
  selector: 'app-generate',
  templateUrl: './generate.component.html',

  styleUrls: ['./generate.component.css']
})
export class GenerateComponnent implements OnInit, OnChanges {
  //#region implement
  ngOnChanges(changes: SimpleChanges): void {
    //console.log(changes);
    //console.log(this.columns);
    //console.log(this.selectedData);
  }
  ngOnInit(): void {
    //console.log("init");
    //console.log(this.columns);
    //console.log(this.selectedData);
  }
  //#endregion
  //#region properties
  @ViewChildren(TextboxComponnent) TextboxChildren: QueryList<IBaseUIComponent>;
  @ViewChildren(DropdownComponnent) DropdownChildren: QueryList<IBaseUIComponent>;
  @ViewChildren(CheckboxComponnent) CheckboxChildren: QueryList<IBaseUIComponent>;
  @ViewChildren(DatePickerComponnent) DatePickerChildren: QueryList<IBaseUIComponent>;
  //#endregion
  //#region input
  @Input() columns: ColumnProperties[];
  @Input() selectedData: any;
  @Input() state: CurrentState;
  //#endregion
  //#region output
  @Output() selectedActionEvent = new EventEmitter<any>();
  @Output() changeViewEvent = new EventEmitter<Action>();
  @Output() submitChangeEvent = new EventEmitter<any>();
  //#endregion
  //#region method
  changeDropDown(event) {
    this.selectedActionEvent.emit(event);
  }
  submitChanged() {
    //console.log("submitchange");
    //console.log(this.children);
    //console.log(this.selectedData);
    this.TextboxChildren.forEach(child => {
      this.selectedData[child.name] = child.inputValue;
    })
    this.DropdownChildren.forEach(child => {
      this.selectedData[child.name] = child.inputValue;
    })
    this.DatePickerChildren.forEach(child => {
      this.selectedData[child.name] = child.inputValue;
    })
    this.CheckboxChildren.forEach(child => {
      this.selectedData[child.name] = child.inputValue;
    })
    console.log(this.selectedData);
    this.submitChangeEvent.emit(this.selectedData);
  }
  backToList() {
    this.changeViewEvent.emit(new fromCmsAction.ChangedView(CurrentState.List, this.selectedData["Id"]));
  }
  //#endregion
}
