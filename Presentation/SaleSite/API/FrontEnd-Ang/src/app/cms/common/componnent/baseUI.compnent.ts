import { Component, DoCheck, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from "@angular/core";

@Component({
  template:''
})
export class BaseUIComponent implements IBaseUIComponent//, OnChanges, DoCheck
{
  constructor() {
  }
  //ngDoCheck(): void {
  //  //this.outputValue.emit(this.inputValue);
  //}
  //ngOnChanges(changes: SimpleChanges): void {
  //  //console.log(changes);
  //}
  //public getItem<IBaseUIComponent>(): IBaseUIComponent {
  //  return this;
  //}
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
  //#region output
  //@Output() outputValue= new EventEmitter<string>();
  //#endregion


}
export interface IBaseUIComponent {
  //getItem<T>(): T;
  placeHolder: string;
  title: string;
  inputType: string;
  inputValue: string;
  data: string;
  name: string;
  isSecurity: boolean;
  isDisabled: boolean;
  isRequired: boolean;
  width: string
  color: string;
}

