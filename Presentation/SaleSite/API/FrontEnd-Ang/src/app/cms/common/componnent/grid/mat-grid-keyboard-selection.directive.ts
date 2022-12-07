import { Directive, ElementRef, Host, HostListener, Input, OnDestroy, OnInit, Self, AfterViewInit, Output, EventEmitter, Inject } from '@angular/core';
import { CurrentState } from '../../constant/constant.common';
import * as fromCmsAction from "../../../main/store/cms-module.action"
import { Action } from '@ngrx/store';

@Directive({
  selector: '[matGridKeyboardSelection]'
})
export class MatGridKeyboardSelectionDirective implements OnInit, OnDestroy {
  constructor(private el: ElementRef,) { }
  
  //#region private property

  //#endregion
  //#region Input

  //#endregion
  //#region output
  @Output() rowActionEvent = new EventEmitter<Action>();
  @Output() openFilterDialog = new EventEmitter<any>();
  //#endregion
  //#region implement
  ngOnInit(): void {

  }
  ngOnDestroy(): void {

  }
  //#endregion
  //#region listener
  @HostListener('keydown', ['$event']) onKeydown(event: KeyboardEvent) {
    let newRow;
    console.log(event.key);
    // this key for edit
    if (event.key === 'Insert') {
      this.rowActionEvent.emit(new fromCmsAction.ChangedView(CurrentState.Insert, null));
    }
    else if (event.key === '+') {
      console.log("+ clicked");
      this.openFilterDialog.emit();
    }
  }
  //#endregion

}

