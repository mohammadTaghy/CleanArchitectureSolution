import { Component, Input, OnInit } from "@angular/core";
import { ColumnProperties } from "../../constant/constant.common";

@Component({
  selector: 'app-textbox',
  templateUrl: './textbox.component.html',

  styleUrls: ['./textbox.component.css']
})
export class TextboxComponnent implements OnInit {
  ngOnInit(): void {

  }
  //#region input
  @Input() placeHolder: string;
  @Input() inputType: string;
  @Input() inputValue: string;
  @Input() data: string;
  @Input() name: string;
  @Input() isSecurity: boolean = false;
  @Input() isDisabled: boolean = false;
  @Input() isRequired: boolean = false;
  @Input() width: string="w-100"
  //#endregion
  //#region properties
  hide: boolean = true;
  //#endregion

}
