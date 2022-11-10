import { Component, OnDestroy, OnInit } from "@angular/core";
import { MatTableDataSource } from "@angular/material/table";
import { Router } from "@angular/router";
import { Action, Store } from "@ngrx/store";
import { Subscription } from "rxjs";
import { map } from "rxjs/operators";
import { ApiAddresses, ApiUrlPostfix } from "../../../../../commonComponent/apiAddresses/apiAddresses.common";

import { Membership_User } from "../../../../../model/membership_user.model";
import { Membership_UserProfile, UserGrid } from "../../../../../model/membership_userProfile.model";
import { ColumnProperties, CurrentState } from "../../../../common/constant/constant.common";
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
  state: number;
  error: string;
  selectedItem: Membership_UserProfile;
  users: MatTableDataSource<Membership_UserProfile>;
  columns: ColumnProperties[] = this.userGrid.gridColumns;
  dialogColumns: ColumnProperties[] = this.userGrid.gridColumns.filter(p => p.isDialogVisible);
  totalData: number;
  pageNumber: number;
  pageSize: number;
  //#endregion
  //#region implement
  ngOnInit(): void {
    //console.log("user");
    this.subscribe = this.store.select("moduleState")
      .subscribe((data) => {
        //console.log("user subscribe");
        //console.log(data);
        this.users = new MatTableDataSource<Membership_UserProfile>(data.items);
        this.totalData = this.users?.data?.length ?? 0;
        this.state = data.currentSatate;
        this.error = data.error;
        console.log(data.selectedData);
        this.selectedItem = data.selectedData ?? new Membership_UserProfile();
        this.pageNumber = data.pageNumber;
        this.pageSize = data.pageSize;
      }
      );
  }
  ngOnDestroy(): void {
    this.subscribe.unsubscribe();
  }
  //#endregion
  constructor(private store: Store<fromCmsApp.CmsState<Membership_UserProfile>>, private router: Router, private apiAddresses: ApiAddresses, private userGrid: UserGrid) {
    this.store.dispatch(
      new fromCmsAction.FetchData(
        "Index=1&PageSize=10&FirstName=&LastName=&MobileNumber=&NationalCode=&UserName=",
        apiAddresses.GetServiceUrl(ApiUrlPostfix.MembershipUsers),
        "Get"));
  }
  //#region method
  actionEvent(action: Action) {
    //console.log(action);
    this.store.dispatch(action);
  }

  //#endregion
}
