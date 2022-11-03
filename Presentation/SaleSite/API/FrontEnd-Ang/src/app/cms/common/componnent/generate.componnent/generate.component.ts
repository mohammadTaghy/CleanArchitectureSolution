import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from "@angular/core";
import { ColumnProperties, CurrentState } from "../../constant/constant.common";

@Component({
  selector: 'app-generate',
  templateUrl: './generate.component.html',

  styleUrls: ['./generate.component.css']
})
export class GenerateComponnent implements OnInit, OnChanges {
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

  //#endregion
}
