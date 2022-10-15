import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { FormsModule } from '@angular/forms';

import { BrowserModule } from "@angular/platform-browser";
import { StoreModule } from "@ngrx/store";
import { EffectsFeatureModule, EffectsModule } from "@ngrx/effects";


import { MatToolbarModule } from "@angular/material/toolbar";
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { MatDrawerContent, MatSidenavContainer, MatSidenavModule } from '@angular/material/sidenav';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input'
import { MatButtonToggleModule } from '@angular/material/button-toggle'
import { MatTooltipModule } from "@angular/material/tooltip";
import { MatTreeModule } from "@angular/material/tree";


import { LoginComponent } from "./auth/login.component";
import { CmsRoutingModule } from "./cms.routing.module";
import { LoginEffects } from "./auth/store/login.effects";
import { LoginGuard } from "./auth/login.guard";
import { CallAPIComponent } from "../commonComponent/callAPI/callAPI.common";
import { ApiAddresses } from "../commonComponent/apiAddresses/apiAddresses.common";
import { AdminPanelComponent } from "./main/AdminPanel/AdminPanel.component";
import { UserCmsPage } from "./main/modules/membership/user/user.component";
import { MainCmsPage } from "./main/main-cms-page.component";

@NgModule({
  declarations: [
    LoginComponent, AdminPanelComponent, UserCmsPage, MainCmsPage
  ],
  providers: [LoginGuard, LoginEffects, CallAPIComponent, ApiAddresses],
  imports: [
    BrowserModule,
    FormsModule,      
    RouterModule,
    CmsRoutingModule,
    MatInputModule, MatFormFieldModule, MatIconModule, MatProgressSpinnerModule, MatButtonModule, MatCardModule,
    MatToolbarModule, MatDividerModule, MatListModule, MatSidenavModule, MatButtonToggleModule, MatSidenavModule, MatTooltipModule,
    MatTreeModule
    
    
  ],
  //,
  //providers: [
  //  CallAPIComponent,
  //  {
  //    provide: HTTP_INTERCEPTORS,
  //    useClass: AuthInterceptorService,
  //    multi: true
  //  }
  //]
})
export class CmsModule { }
