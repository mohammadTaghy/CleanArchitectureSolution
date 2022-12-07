import { Component, Input, OnChanges, OnInit, SimpleChanges } from "@angular/core";
import { ColumnProperties } from "../../constant/constant.common";
import { BaseUIComponent } from "../baseUI.compnent";

@Component({
  selector: 'app-textbox',
  templateUrl: './textbox.component.html',

  styleUrls: ['./textbox.component.css']
})
export class TextboxComponnent extends BaseUIComponent implements OnInit, OnChanges {
  ngOnChanges(changes: SimpleChanges): void {
  }
  ngOnInit(): void {

  }
 
  //#region properties
  hide: boolean = true;
  //#endregion

}
