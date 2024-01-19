import { Component, OnDestroy, OnInit } from "@angular/core";
import { MatTableDataSource } from "@angular/material/table";
import { Router } from "@angular/router";
import { Action, Store } from "@ngrx/store";
import { Subscription } from "rxjs";
import { ApiAddresses, ApiUrlPostfix } from "../../../../../commonComponent/apiAddresses/apiAddresses.common";

import { Common_LessonsCategories, LessonsCategoriesGrid } from "../../../../../model/common/common_lessonsCategories";

import { ColumnProperties, IFilterData, ISortData } from "../../../../common/constant/constant.common";
import { CurrentState } from "../../../../common/constant/enum.common";
import * as fromCmsApp from "../../../../store/cms.reducer"
import * as fromCmsAction from "../../../store/cms-module.action"

@Component({
  selector: 'common-lessonsCategories-cms',
  templateUrl: './lessonsCategories.component.html',
  styleUrls: ['./lessonsCategories.component.scss']
})
export class UserCmsPage implements OnInit, OnDestroy {

  //#region properties
  subscribe: Subscription;
  state: number = CurrentState.List;
  error: string = "";
  selectedItem: Common_LessonsCategories;
  dataSource: MatTableDataSource<Common_LessonsCategories>;
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
  constructor(private store: Store<fromCmsApp.CmsState<Common_LessonsCategories>>, private router: Router, private apiAddresses: ApiAddresses, private userGrid: LessonsCategoriesGrid) {
    this.loadData();
  }
  //#region method
  actionEvent(action: Action) {
    //console.log(action);
    this.store.dispatch(action);
  }
  submitEvent(data: Common_LessonsCategories) {
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
            this.apiAddresses.GetServiceUrl(ApiUrlPostfix.MembershipUsers) + "/" + data.id));
        break;
      case CurrentState.Delete:
        this.store.dispatch(
          new fromCmsAction.DeleteRequestStart(
            this.apiAddresses.GetServiceUrl(ApiUrlPostfix.MembershipUsers) + "/" + data.id));
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
        this.dataSource = new MatTableDataSource<Common_LessonsCategories>(data.items);
        this.totalData = data.totalCount;
        this.state = data.currentSatate;
        this.error = data.error;
        console.log(data.selectedData);
        this.selectedItem = data.selectedData != null ? data.selectedData : new Common_LessonsCategories("", "", true, "", 0, 0);
        console.log(this.selectedItem);
        this.filter = data.currentFilter;
        this.sort = data.currentSort;
      });
  }
  loadData() {
    //console.log("load User");
    let url: string = this.apiAddresses.GetServiceUrl(ApiUrlPostfix.LessonsCategories);
    this.store.dispatch(new fromCmsAction.FetchData(url, this.filter, this.sort));
  }
  setFilter(filters: IFilterData[]) {
   //console.log("setFilter");
    this.filter = filters;
    this.loadData();
  }
  //#endregion
}
