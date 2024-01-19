import { Component, EventEmitter, Input, OnChanges, OnInit, Output, QueryList, SimpleChanges, ViewChildren } from "@angular/core";
import { Action } from "@ngrx/store";

import { ColumnProperties } from "../../constant/constant.common";
import { BaseUIComponent, IBaseUIComponent } from "../baseUI.compnent";
import * as fromCmsAction from "../../../main/store/cms-module.action"
import { TextboxComponnent } from "../textbox/textbox.component";
import { DropdownComponnent } from "../dropdown/dropdown.component";
import { CheckboxComponnent } from "../checkbox/checkbox.component";
import { DatePickerComponnent } from "../datepicker/datepicker.component";
import { CurrentState } from "../../constant/enum.common";
import { FileUploadComponnent } from "../uploadfile/fileUpload.component";

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
  @ViewChildren(TextboxComponnent) textboxChildren: QueryList<IBaseUIComponent>;
  @ViewChildren(DropdownComponnent) dropdownChildren: QueryList<IBaseUIComponent>;
  @ViewChildren(CheckboxComponnent) checkboxChildren: QueryList<IBaseUIComponent>;
  @ViewChildren(DatePickerComponnent) datePickerChildren: QueryList<IBaseUIComponent>;
  @ViewChildren(FileUploadComponnent) fileUploadChildren: QueryList<IBaseUIComponent>;
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
    let submitEntity = { ...this.selectedData };
    this.textboxChildren.forEach(child => {
      submitEntity[child.name] = child.inputValue;
    })
    this.dropdownChildren.forEach(child => {
      submitEntity[child.name] = child.inputValue;
    })
    this.datePickerChildren.forEach(child => {
      submitEntity[child.name] = child.inputValue;
    })
    this.checkboxChildren.forEach(child => {
      submitEntity[child.name] = child.inputValue;
    })
    this.fileUploadChildren.forEach(child => {
      submitEntity[child.name] = child.inputValue;
    })
    submitEntity["id"] = null;
    console.log(submitEntity);
    this.submitChangeEvent.emit(submitEntity);
  }
  backToList() {
    this.changeViewEvent.emit(new fromCmsAction.ChangedView(CurrentState.List, this.selectedData));
  }
  //#endregion
}
