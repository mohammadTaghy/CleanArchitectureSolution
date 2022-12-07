import { Component, OnDestroy, OnInit } from "@angular/core";
import { MatTableDataSource } from "@angular/material/table";
import { Router } from "@angular/router";
import { Action, Store } from "@ngrx/store";
import { Subscription } from "rxjs";
import { map } from "rxjs/operators";
import { ApiAddresses, ApiUrlPostfix } from "../../../../../commonComponent/apiAddresses/apiAddresses.common";

import { Membership_User } from "../../../../../model/membership_user.model";
import { Membership_UserProfile, UserGrid } from "../../../../../model/membership_userProfile.model";
import { ColumnProperties, CurrentState, IFilterData, ISortData } from "../../../../common/constant/constant.common";
import { CmsContext } from "../../../../common/context/cms-context";
import { FilterRequestBody, QueryRequestBody } from "../../../../common/filterRequestBody/filter-request-body.common";
import * as fromCmsApp from "../../../../store/cms.reducer"
import * as fromCmsAction from "../../../store/cms-module.action"

@Component({
  selector: 'membership-user-cms',
  templateUrl: './user.component.html'
})
export class UserCmsPage implements OnInit, OnDestroy {

  //#region properties
  subscribe: Subscription;
  state: number = CurrentState.List;
  error: string = "";
  selectedItem: Membership_UserProfile;
  users: MatTableDataSource<Membership_UserProfile>;
  columns: ColumnProperties[] = this.userGrid.gridColumns;
  dialogColumns: ColumnProperties[] = this.userGrid.gridColumns.filter(p => p.isDialogVisible);
  totalData: number = 0;
  pageNumber: number = 1;
  pageSize: number=10;
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
  constructor(private store: Store<fromCmsApp.CmsState<Membership_UserProfile>>, private router: Router, private apiAddresses: ApiAddresses, private userGrid: UserGrid) {
    this.loadData();
  }
  //#region method
  actionEvent(action: Action) {
    //console.log(action);
    this.store.dispatch(action);
  }
  submitEvent(data: Membership_UserProfile) {
    switch (this.state) {
      case CurrentState.Insert:
        console.log("insert");
        console.log(data);
        this.store.dispatch(
          new fromCmsAction.AddRequestStart(
            data,
            this.apiAddresses.GetServiceUrl(ApiUrlPostfix.MembershipUsers)));
        break;
      case CurrentState.Edit:
        this.store.dispatch(
          new fromCmsAction.EditRequestStart(
            data,
            this.apiAddresses.GetServiceUrl(ApiUrlPostfix.MembershipUsers) + "/" + data.Id));
        break;
      case CurrentState.Delete:
        this.store.dispatch(
          new fromCmsAction.DeleteRequestStart(
            this.apiAddresses.GetServiceUrl(ApiUrlPostfix.MembershipUsers) + "/" + data.Id));
        break;
      default:
        this.loadData();
        break;
    }
  }
  bundData() {
    this.subscribe = this.store.select("moduleState")
      .subscribe((data) => {
        console.log("user subscribe");
        //console.log(data);
        this.users = new MatTableDataSource<Membership_UserProfile>(data.items);
        this.totalData = this.users?.data?.length ?? 0;
        this.state = data.currentSatate;
        this.error = data.error;
        console.log(data.selectedData);
        this.selectedItem = data.selectedData != null ? data.selectedData : new Membership_UserProfile(
          null, "مرد", "1", "", "", "", "", "", "", "", "", "", "", "", "", true, true, true,1,""
        );
        console.log(this.selectedItem);
        this.pageNumber = data.pageNumber;
        this.pageSize = data.pageSize;
        this.filter = data.currentFilter;
        this.sort = data.currentSort;
      });
  }
  loadData() {
   //console.log("load User");
    let skip: number = (this.pageNumber - 1) * this.pageSize;
    let url: string = this.apiAddresses.GetServiceUrl(ApiUrlPostfix.MembershipUsers) +
      "&Top=" + this.pageSize +
      "&Skip=" + skip;

    this.store.dispatch(new fromCmsAction.FetchData(url, this.filter, this.sort));
  }
  setFilter(filters: IFilterData[]) {
   //console.log("setFilter");
    this.filter = filters;
    this.loadData();
  }
  //#endregion
}
