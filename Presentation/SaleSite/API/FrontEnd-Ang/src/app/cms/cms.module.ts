import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { BrowserModule } from "@angular/platform-browser";
import { StoreModule } from "@ngrx/store";
import { EffectsFeatureModule, EffectsModule } from "@ngrx/effects";

import { LoginComponent } from "./auth/login.component";
import { CmsRoutingModule } from "./cms.routing.module";
import * as fromLogin from './auth/store/login.reducer';
import * as fromLoginEffect from './auth/store/login.effects';
import { LoginEffects } from "./auth/store/login.effects";
//import * as fromCmsReducer from "./store/cms.reducer";
import { LoginGuard } from "./auth/login.guard";
import { CallAPIComponent } from "../commonComponent/callAPI/callAPI.common";
import { ApiAddresses } from "../commonComponent/apiAddresses/apiAddresses.common";

@NgModule({
  declarations: [
    LoginComponent
  ],
  providers: [LoginGuard, LoginEffects, CallAPIComponent,
    ApiAddresses],
  imports: [
    BrowserModule,
    FormsModule,      
    RouterModule,
    CmsRoutingModule,
    
    MatInputModule, MatFormFieldModule, MatIconModule, MatProgressSpinnerModule,
    
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
