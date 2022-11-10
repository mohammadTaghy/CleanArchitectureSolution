import { Component, Input, OnInit } from "@angular/core";

@Component({
  template:''
})
export class BaseUIComponent implements OnInit {
  ngOnInit(): void {
      
  }
  //#region input
  @Input() placeHolder: string;
  @Input() title: string;
  @Input() inputType: string;
  @Input() inputValue: string;
  @Input() data: string;
  @Input() name: string;
  @Input() isSecurity: boolean = false;
  @Input() isDisabled: boolean = false;
  @Input() isRequired: boolean = false;
  @Input() width: string = "w-100"
  @Input() color: string;

  //#endregion
}
