import { Component, Input, OnInit } from "@angular/core";

@Component({
  selector: 'app-checkbox',
  templateUrl: './checkbox.component.html',
  styleUrls: ['./checkbox.component.css']
})
export class CheckboxComponnent implements OnInit {
  ngOnInit(): void {

  }
  //#region input
  @Input() color: string;
  @Input() title: string;
  @Input() inputValue: boolean;
  @Input() name: string;
  @Input() disabled: boolean = false;

  //#endregion
  //#region properties
  //#endregion

}
