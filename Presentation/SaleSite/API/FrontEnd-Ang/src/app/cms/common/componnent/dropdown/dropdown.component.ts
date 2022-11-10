import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from "@angular/core";
import { FormControl, Validators } from "@angular/forms";
import * as constant  from "../../constant/constant.common";
import { BaseUIComponent } from "../baseUI.compnent";

@Component({
  selector: 'app-dropdown',
  templateUrl: './dropdown.component.html',

  styleUrls: ['./dropdown.component.css']
})
export class DropdownComponnent extends BaseUIComponent implements OnInit, OnChanges {
  ngOnChanges(changes: SimpleChanges): void {
    
  }
  ngOnInit(): void {
    console.log("dropdown");
    console.log(this.dataSource);
  }
  //#region input
  @Input() dataSource: constant.dropdownDataType[];
  //#endregion
  //#region output
  @Output() selectedActionEvent = new EventEmitter<any>();
  //#endregion
  //#region properties
  dropdownControl = new FormControl<constant.dropdownDataType | null>(null, Validators.required);
  selectFormControl = new FormControl('', Validators.required);
  //#endregion
  //#region method
  actionEvent(event: any) {
    this.selectedActionEvent.emit(event);
  }
  //#endregion
}

