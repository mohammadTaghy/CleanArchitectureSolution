import { Component, OnDestroy, OnInit } from "@angular/core";
import { MatTableDataSource } from "@angular/material/table";
import { Router } from "@angular/router";
import { Action, Store } from "@ngrx/store";
import { Subscription } from "rxjs";
import { ApiAddresses, ApiUrlPostfix } from "../../../../../commonComponent/apiAddresses/apiAddresses.common";
import { Membership_Permission, PermissionGrid } from "../../../../../model/membership/membership_permission.model";


import { ColumnProperties, IFilterData, ISortData } from "../../../../common/constant/constant.common";
import { CurrentState } from "../../../../common/constant/enum.common";
import * as fromCmsApp from "../../../../store/cms.reducer"
import * as fromCmsAction from "../../../store/cms-module.action"

@Component({
  selector: 'membership-permission-cms',
  templateUrl: './permission.component.html',
  styleUrls: ['./permission.component.css']
})
export class PermissionsCmsPage implements OnInit, OnDestroy {

  //#region properties
  subscribe: Subscription;
  state: number = CurrentState.List;
  error: string = "";
  selectedItem: Membership_Permission;
  dataSource: Membership_Permission[];
  columns: ColumnProperties[] = this.userGrid.gridColumns;
  dialogColumns: ColumnProperties[] = this.userGrid.gridColumns.filter(p => p.isDialogVisible==true);
  totalData: number = 0;
  filter: IFilterData[];
  sort: ISortData[] ;
  //#endregion
  //#region implement
  ngOnInit(): void {
    //console.log("user");
    this.bundData();
  }
  ngOnDestroy(): void {
    this.subscribe.unsubscribe();
  }
  //#endregion
  constructor(private store: Store<fromCmsApp.CmsState<Membership_Permission>>,
    private apiAddresses: ApiAddresses, private userGrid: PermissionGrid) {
   //console.log("permission Constructor");
    this.loadData();
  }
  //#region method
  actionEvent(action: Action) {
    //console.log(action);
    this.store.dispatch(action);
  }
  setSelectedItem(data) {
    this.selectedItem = data;
  }
  submitEvent(data: Membership_Permission) {
    switch (this.state) {
      case CurrentState.Insert:
        console.log("insert");
        console.log(data);
        console.log(this.selectedItem);
        data.parentId = this.selectedItem != null ? this.selectedItem.id : null;
        console.log(data);
        this.store.dispatch(
          new fromCmsAction.AddRequestStart(
            data,
            this.apiAddresses.GetServiceUrl(ApiUrlPostfix.Permissions)));
        break;
      case CurrentState.Edit:
        this.store.dispatch(
          new fromCmsAction.EditRequestStart(
            data,
            this.apiAddresses.GetServiceUrl(ApiUrlPostfix.Permissions) + "/" + this.selectedItem.id));
        break;
      case CurrentState.Delete:
        this.store.dispatch(
          new fromCmsAction.DeleteRequestStart(
            this.apiAddresses.GetServiceUrl(ApiUrlPostfix.Permissions) + "/" + this.selectedItem.id));
        break;
      default:
        this.loadData();
        break;
    }
  }
  bundData() {
    this.subscribe = this.store.select("moduleState")
      .subscribe((data) => {
      //console.log("permission subscribe");
      //console.log(data);
        this.dataSource = data.items;
      console.log(this.dataSource);
        this.totalData = data.totalCount;
        this.state = data.currentSatate;
        this.error = data.error;
       //console.log(data.selectedData);
        this.selectedItem = data.selectedData != null ? data.selectedData : new Membership_Permission("", "", "", 0, true, false);
       //console.log(this.selectedItem);
        this.filter = data.currentFilter;
        this.sort = data.currentSort;
      });
  }
  loadData() {
   //console.log("load permission");
    let url: string = this.apiAddresses.GetServiceUrl(ApiUrlPostfix.Permissions);
    this.store.dispatch(new fromCmsAction.FetchData(url, this.filter, this.sort));
  }
  setFilter(filters: IFilterData[]) {
   //console.log("setFilter");
    this.filter = filters;
    this.loadData();
  }
  //#endregion
}
