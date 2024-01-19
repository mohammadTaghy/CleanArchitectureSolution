import { Action } from "@ngrx/store";
import { CurrentState } from "../constant/enum.common";

export class CmsContext<T> {
  constructor(public stateName: Action) {

  }
}
