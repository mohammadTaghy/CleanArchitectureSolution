import { Component, ComponentFactoryResolver, Injectable, OnInit, TemplateRef, ViewChild } from "@angular/core";
import { NgForm, ReactiveFormsModule, FormsModule } from "@angular/forms";

import { Store } from "@ngrx/store";
import { Subscription } from "rxjs";


import { AlertComponent } from "../../commonComponent/alert/alert.component";
import { PlaceholderDirective } from "../../commonComponent/placeholder/placeholder.directive";
import * as fromCmsApp from "../store/cms.reducer"
import * as fromLoginAction from "./store/login.action"

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
@Injectable()
export class LoginComponent implements OnInit {
  @ViewChild(PlaceholderDirective, { static: false }) alertHost: PlaceholderDirective;
  private closeSub: Subscription;
  public isLoading: boolean;
  public hide: boolean;
  public loginForm;
  constructor(
    private store: Store<fromCmsApp.CmsState>,
    private componentFactoryResolver: ComponentFactoryResolver) {
    console.log("LoginComponent");
    this.isLoading = false;
    this.hide = false;
  }
  ngOnInit(): void {
    console.log("LoginComponent")
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
}
