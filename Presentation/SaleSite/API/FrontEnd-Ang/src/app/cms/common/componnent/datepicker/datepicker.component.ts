import { Component, Input, OnInit } from "@angular/core";
import { ColumnProperties } from "../../constant/constant.common";
import { BaseUIComponent } from "../baseUI.compnent";

@Component({
  selector: 'app-datepicker',
  templateUrl: './datepicker.component.html',

  styleUrls: ['./datepicker.component.css']
})
export class DatePickerComponnent extends BaseUIComponent implements OnInit {
  ngOnInit(): void {

  }
  //#region input
  @Input() timeEnable: boolean = false;
  @Input() timeShowSecond: boolean = false;
  @Input() timeMeridian: boolean = false;
  //#endregion
  //#region properties
  hide: boolean = true;
  //#endregion

}
