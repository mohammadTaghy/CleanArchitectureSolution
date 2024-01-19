import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { StoreRouterConnectingModule } from '@ngrx/router-store';
import { EffectsModule, EffectsFeatureModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CommonComponentModule } from './commonComponent/common.module';
import { environment } from '../environments/environment';

import { LoginEffects } from './cms/auth/store/login.effects';
import * as fromCmsApp from "./cms/store/cms.reducer"
import { AuthInterceptorService } from './cms/auth/login.interceptor';
import { AdminPanelEffects } from './cms/main/AdminPanel/store/adminPanel.effects';
import { CmsModuleEffects } from './cms/main/store/cms-module.effects';
import { RouterModule } from '@angular/router';



@NgModule({
  providers: [
    {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptorService,
    multi: true
    },
  ],
  declarations: [
    AppComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    BrowserAnimationsModule,
    CommonModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    EffectsFeatureModule,
    EffectsModule.forRoot([]),
    StoreModule.forRoot([]),
    AppRoutingModule,
    CommonComponentModule,
    StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: environment.production }),
    StoreModule.forRoot(fromCmsApp.cmsReducer),
    EffectsModule.forFeature([LoginEffects, AdminPanelEffects, CmsModuleEffects]),

    StoreRouterConnectingModule.forRoot(),

    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
