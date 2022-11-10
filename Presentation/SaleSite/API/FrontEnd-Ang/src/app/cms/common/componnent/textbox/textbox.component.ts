import { Component, Input, OnInit } from "@angular/core";
import { ColumnProperties } from "../../constant/constant.common";
import { BaseUIComponent } from "../baseUI.compnent";

@Component({
  selector: 'app-textbox',
  templateUrl: './textbox.component.html',

  styleUrls: ['./textbox.component.css']
})
export class TextboxComponnent extends BaseUIComponent implements OnInit {
  ngOnInit(): void {

  }
 
  //#region properties
  hide: boolean = true;
  //#endregion

}
