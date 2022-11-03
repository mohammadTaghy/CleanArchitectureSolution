import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from "@angular/core";
import { FormControl, Validators } from "@angular/forms";
import * as constant  from "../../constant/constant.common";

@Component({
  selector: 'app-dropdown',
  templateUrl: './dropdown.component.html',

  styleUrls: ['./dropdown.component.css']
})
export class DropdownComponnent implements OnInit, OnChanges {
  ngOnChanges(changes: SimpleChanges): void {
    
  }
  ngOnInit(): void {
    console.log("dropdown");
  }
  //#region input
  @Input() title: string;
  @Input() name: string;
  @Input() inputValue: string;
  @Input() isDisabled: boolean = false;
  @Input() isRequired: boolean = false;
  @Input() dataSource: constant.dropdownDataType[];
  @Input() width: string = "w-100"

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

