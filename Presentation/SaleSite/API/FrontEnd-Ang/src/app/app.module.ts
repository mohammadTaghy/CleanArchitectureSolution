import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CommonComponentModule } from './commonComponent/common.module';
import { CmsModule } from './cms/cms.module';
import { environment } from '../environments/environment';
import { StoreRouterConnectingModule } from '@ngrx/router-store';
import { EffectsModule, EffectsFeatureModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { LoginEffects } from './cms/auth/store/login.effects';
import * as fromLogin from './cms/auth/store/login.reducer';
import * as fromCmsApp from "./cms/store/cms.reducer"



@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    BrowserAnimationsModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    EffectsFeatureModule,
    EffectsModule.forRoot([]),
    StoreModule.forRoot([]),
    AppRoutingModule,


    CommonComponentModule,

    

    StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: environment.production }),
    StoreModule.forRoot(fromCmsApp.cmsReducer),
    EffectsModule.forFeature([LoginEffects]),

    StoreRouterConnectingModule.forRoot(),

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
