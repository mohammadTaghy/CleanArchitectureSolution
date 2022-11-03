import { Component, ComponentFactoryResolver, Injectable, NgModule, OnDestroy, OnInit, QueryList, TemplateRef, ViewChild, ViewChildren } from "@angular/core";
import { NgForm } from "@angular/forms";

import { Store } from "@ngrx/store";
import { Subscription } from "rxjs";


import { AlertComponent } from "../../commonComponent/alert/alert.component";
import { PlaceholderDirective } from "../../commonComponent/placeholder/placeholder.directive";
import { TextboxComponnent } from "../common/componnent/textbox/textbox.component";
import * as fromCmsApp from "../store/cms.reducer"
import * as fromLoginAction from "./store/login.action"

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['/login.component.css'],
})


export class LoginComponent implements OnInit, OnDestroy {
  @ViewChild(PlaceholderDirective, { static: false }) alertHost: PlaceholderDirective;
  @ViewChildren(TextboxComponnent) children: QueryList<TextboxComponnent>;
  private closeSub: Subscription;
  private storeSub: Subscription;

  public isLoading: boolean;
  public hide: boolean;
  public loginForm;
  constructor(
    private store: Store<fromCmsApp.CmsState<any>>,
    private componentFactoryResolver: ComponentFactoryResolver) {
    console.log("login");
    this.isLoading = false;
    this.hide = true;
  }
 
  ngOnInit(): void {
    this.storeSub = this.store.select('loginState').subscribe(state => {
      this.isLoading = state.loading;
      if (state.authError)
        this.showErrorAlert(state.authError);
    })
   
  }
  onSubmit(loginForm: NgForm) {
    if (!loginForm.valid) return;
    console.log(this.children);
    console.log(this.children.find(p => p.name == "userName").inputValue);
    console.log("value");
    
    this.store.dispatch(new fromLoginAction.LoginStart({
      userName: this.children.find(p => p.name == "userName").inputValue,
      password: this.children.find(p => p.name == "password").inputValue
    }))
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
    this.storeSub.unsubscribe();
  }
}
