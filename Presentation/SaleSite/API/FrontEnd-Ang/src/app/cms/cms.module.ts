import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { BrowserModule } from "@angular/platform-browser";
import { StoreModule } from "@ngrx/store";
import { EffectsFeatureModule, EffectsModule } from "@ngrx/effects";


import { MatToolbarModule } from "@angular/material/toolbar";
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input'
import { MatButtonToggleModule } from '@angular/material/button-toggle'
import { MatTooltipModule } from "@angular/material/tooltip";
import { MatTreeModule } from "@angular/material/tree";
import { MatTableModule } from "@angular/material/table";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatSortModule } from "@angular/material/sort";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatSelectModule } from "@angular/material/select";
import { MatDialogModule } from "@angular/material/dialog";
import { NgPersianDatepickerModule } from "ng-persian-datepicker";
import { NgxMatSelectSearchModule } from "ngx-mat-select-search";



import { LoginComponent } from "./auth/login.component";
import { CmsRoutingModule } from "./cms.routing.module";
import { LoginEffects } from "./auth/store/login.effects";
import { LoginGuard } from "./auth/login.guard";
import { CallAPIComponent } from "../commonComponent/callAPI/callAPI.common";
import { ApiAddresses } from "../commonComponent/apiAddresses/apiAddresses.common";
import { AdminPanelComponent } from "./main/AdminPanel/AdminPanel.component";
import { UserCmsPage } from "./main/modules/membership/user/user.component";
import { MainCmsPage } from "./main/main-cms-page.component";
import { GridComponnent } from "./common/componnent/grid/grid.componnent";
import { UserGrid } from "../model/membership/membership_userProfile.model";
import { MatRowKeyboardSelectionDirective } from "./common/componnent/mat-row-keyboard-selection.directive";
import { TreeComponent } from "./common/componnent/tree/tree.component";
import { TextboxComponnent } from "./common/componnent/textbox/textbox.component";
import { GenerateComponnent } from "./common/componnent/generate.componnent/generate.component";
import { CheckboxComponnent } from "./common/componnent/checkbox/checkbox.component";
import { DropdownComponnent } from "./common/componnent/dropdown/dropdown.component";
import { DatePickerComponnent } from "./common/componnent/datepicker/datepicker.component";
import { FilterDialogComponnent } from "./common/componnent/grid/filter_dialog/filter_dialog.component";
import { BaseUIComponent } from "./common/componnent/baseUI.compnent";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { PermissionsCmsPage } from "./main/modules/membership/permission/permission.component";
import { PermissionGrid } from "../model/membership/membership_permission.model";
import { FileUploadComponnent } from "./common/componnent/uploadfile/fileUpload.component";

@NgModule({
  declarations: [
    BaseUIComponent,
    LoginComponent, AdminPanelComponent,
    UserCmsPage, MainCmsPage, PermissionsCmsPage,
    GridComponnent,
    MatRowKeyboardSelectionDirective,
    TreeComponent, TextboxComponnent, GenerateComponnent,
    CheckboxComponnent, DropdownComponnent, DatePickerComponnent, FilterDialogComponnent, FileUploadComponnent
  ],
  providers: [LoginGuard, LoginEffects, CallAPIComponent, ApiAddresses, UserGrid, PermissionGrid],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    CmsRoutingModule,
    NgPersianDatepickerModule,
    MatInputModule, MatFormFieldModule, MatIconModule, MatProgressSpinnerModule, MatButtonModule,
    MatCardModule, MatToolbarModule, MatDividerModule, MatSidenavModule, MatListModule, MatButtonToggleModule,
    MatTooltipModule, MatTreeModule, MatTableModule, MatPaginatorModule, MatSortModule, MatSelectModule,
    MatCheckboxModule, MatDialogModule,
    NgxMatSelectSearchModule

    
    
  ],
  exports:[]
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
