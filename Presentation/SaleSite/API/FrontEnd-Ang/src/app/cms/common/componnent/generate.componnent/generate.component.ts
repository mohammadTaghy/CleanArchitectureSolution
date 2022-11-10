import { Component, EventEmitter, Input, OnChanges, OnInit, Output, QueryList, SimpleChanges, ViewChildren } from "@angular/core";
import { ColumnProperties, CurrentState } from "../../constant/constant.common";
import { BaseUIComponent } from "../baseUI.compnent";

@Component({
  selector: 'app-generate',
  templateUrl: './generate.component.html',

  styleUrls: ['./generate.component.css']
})
export class GenerateComponnent implements OnInit, OnChanges {
  //#region implement
  ngOnChanges(changes: SimpleChanges): void {
    console.log(changes);
    console.log(this.columns);
    console.log(this.selectedData);
  }
  ngOnInit(): void {
    console.log("init");
    console.log(this.columns);
    console.log(this.selectedData);
  }
  //#endregion
  //#region properties
  @ViewChildren(BaseUIComponent) children: QueryList<BaseUIComponent>;
  //#endregion
  //#region input
  @Input() columns: ColumnProperties[];
  @Input() selectedData: any;
  @Input() state: CurrentState;
  //#endregion
  //#region output
  @Output() selectedActionEvent = new EventEmitter<any>();
  //#endregion
  //#region method
  changeDropDown(event) {
    this.selectedActionEvent.emit(event);
  }
  submitChanged() {
    console.log(this.selectedData);
    this.children.forEach(child => {
      this.selectedData[child.name] = child.inputValue;
    })
    console.log(this.selectedData);
  }
  //#endregion
}
