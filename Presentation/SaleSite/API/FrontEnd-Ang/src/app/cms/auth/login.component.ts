import { Component, ComponentFactoryResolver, Injectable, OnDestroy, OnInit, TemplateRef, ViewChild } from "@angular/core";
import { NgForm } from "@angular/forms";

import { Store } from "@ngrx/store";
import { Subscription } from "rxjs";


import { AlertComponent } from "../../commonComponent/alert/alert.component";
import { PlaceholderDirective } from "../../commonComponent/placeholder/placeholder.directive";
import * as fromCmsApp from "../store/cms.reducer"
import * as fromLoginAction from "./store/login.action"

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [
    `
      :host {
        display: flex;
        justify-content: center;
        margin: 100px 0px;
      }

      .mat-form-field {
        width: 100%;
        min-width: 300px;
      }

      mat-card-title,
      mat-card-content {
        display: flex;
        justify-content: center;
      }

      .error {
        padding: 16px;
        width: 300px;
        color: white;
        background-color: red;
      }

      .button {
        display: flex;
        justify-content: flex-end;
      }
    `,
  ],
})
@Injectable()
export class LoginComponent implements OnInit, OnDestroy {
  @ViewChild(PlaceholderDirective, { static: false }) alertHost: PlaceholderDirective;
  private closeSub: Subscription;
  private storeSub: Subscription;

  public isLoading: boolean;
  public hide: boolean;
  public loginForm;
  constructor(
    private store: Store<fromCmsApp.CmsState>,
    private componentFactoryResolver: ComponentFactoryResolver) {
    console.log("LoginComponent");
    this.isLoading = false;
    this.hide = true;
  }
 
  ngOnInit(): void {
    console.log("LoginComponent");
    this.storeSub = this.store.select('loginState').subscribe(state => {
      console.log("loginState subscribe");
      console.log(state);
      this.isLoading = state.loading;
      if (state.authError)
        this.showErrorAlert(state.authError);
    })
  }
  onSubmit(loginForm: NgForm) {
    console.log("submit");
    console.log(loginForm.valid);
    if (!loginForm.valid) return;
    console.log("start");
    this.store.dispatch(new fromLoginAction.LoginStart({ userName: loginForm.value.userName, password: loginForm.value.password }))
  }
  
  private showErrorAlert(message: string) {
    // const alertCmp = new AlertComponent();
    const alertCmpFactory = this.componentFactoryResolver.resolveComponentFactory(
      AlertComponent
    );
    const hostViewContainerRef = this.alertHost.viewContainerRef;
    hostViewContainerRef.clear();

    const componentRef = hostViewContainerRef.createComponent(alertCmpFactory);

    componentRef.instance.message = message;
    this.closeSub = componentRef.instance.close.subscribe(() => {
      this.closeSub.unsubscribe();
      hostViewContainerRef.clear();
    });
  }
  ngOnDestroy(): void {
    console.log("destroy");
    this.storeSub.unsubscribe();
  }
}
