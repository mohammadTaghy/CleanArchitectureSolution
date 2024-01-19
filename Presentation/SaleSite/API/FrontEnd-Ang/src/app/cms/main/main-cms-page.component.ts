import { Component, NgModule, OnInit } from "@angular/core";
import { Store } from "@ngrx/store";

import * as fromCmsApp from "../store/cms.reducer"
import { AdminPanelComponent } from "./AdminPanel/AdminPanel.component";
import { LoginComponent } from "../auth/login.component";
import * as fromLoginAction from "../auth/store/login.action"
import { tap } from "rxjs/operators";

@Component({
  selector: 'main-page',
  templateUrl: './main-cms-page.component.html'
})

export class MainCmsPage implements OnInit {
  isLogin: boolean;
  constructor(private store: Store<fromCmsApp.CmsState<any>>) {
    this.isLogin = false;
  }
  ngOnInit(): void {
    console.log(this.isLogin);
    //this.store.dispatch(new fromLoginAction.AutoLogin());
    console.log("after auto login");
    this.store.select('loginState').pipe(
      tap((user) => {
        console.log("check login");
        this.isLogin = !!user;
      })
    )
  }
}
