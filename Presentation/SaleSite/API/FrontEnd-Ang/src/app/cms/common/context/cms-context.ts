import { Action } from "@ngrx/store";
import { CurrentState } from "../constant/constant.common";

export class CmsContext<T> {
  constructor(public stateName: Action) {

  }
}
