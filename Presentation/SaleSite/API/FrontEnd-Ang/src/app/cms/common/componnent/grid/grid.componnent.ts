import { SelectionModel } from "@angular/cdk/collections";
import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output, ViewChild } from "@angular/core";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { MatSort, Sort } from "@angular/material/sort";
import { MatTableDataSource, MatTable } from "@angular/material/table";
import { Action } from "@ngrx/store";
import { ColumnProperties, IFilterData } from "../../constant/constant.common";
import { FilterDialogComponnent } from "./filter_dialog/filter_dialog.component";
import * as fromCmsAction from "../../../main/store/cms-module.action"
import { CurrentState } from "../../constant/enum.common";

@Component({
  selector: 'app-grid',
  templateUrl: './grid.componnent.html',
  styleUrls: ['./grid.componnent.scss']
})
export class GridComponnent implements OnInit {
  constructor(private cdr: ChangeDetectorRef, private dialog: MatDialog) {
    this.selection = new SelectionModel<ColumnProperties>(true);
  }
  ngOnInit(): void {
    this.displayedColumns = this.columns != null ? this.columns.map(p => p.header) : ["در حال لود"];
  }
  @ViewChild('empTbSortWithObject') empTbSortWithObject = new MatSort();
  @ViewChild('empTbSort') empTbSort = new MatSort();
  //#region input
  @Input() columns: ColumnProperties[];
  @Input() dataSource: MatTableDataSource<any>;
  @Input() totalData: number;
  //#endregion
  //#region output
  @Output() rowActionEvent = new EventEmitter<Action>();
  @Output() setFilterAction = new EventEmitter<IFilterData[]>();

  //#endregion
  //#region property
  displayedColumns: string[];
  height = "400px";
  isPagerHidden = false;
  selectOptions = [10, 15, 20, 50];
  selection: SelectionModel<ColumnProperties>;
  filterData: IFilterData[] = [];
  selected :any;

  //#endregion
  //#region method
  addNewItem() {
    console.log("add new Item");
    this.actionEvent(new fromCmsAction.ChangedView(CurrentState.Insert, null));
  }
  deleteItem() {
    //console.log(this.dataSource.data[this.selectedId]);
    this.actionEvent(new fromCmsAction.ChangedView(CurrentState.Delete, this.selected));
  }
  editItem() {
    //console.log(this.dataSource.data);
    //console.log(this.selectedId);
    //console.log(this.dataSource.data.find(p => p.id == this.selectedId));
    this.actionEvent(new fromCmsAction.ChangedView(CurrentState.Edit,  this.selected));
  }
  openFilterDialog() {
    this.onFilter("");
  }
  //#endregion
  //#region event
  onFilter(event: any) {
   //console.log("open dialog");
    const dialogRef = this.dialog.open(FilterDialogComponnent);
    dialogRef.componentInstance.columns = this.columns.filter(p => p.isGridVisible);
    dialogRef.componentInstance.filters = this.filterData;
    dialogRef.componentInstance.filterDataResults.subscribe(data => {
      //console.log(data);
      this.filterData = data;
      this.setFilterAction.emit(data)
    });

  }
  selectedRowId(selected: any) {
    //console.log("recive id" + id);
    this.selected = selected;
  }
  actionEvent(action: Action) {
    //console.log(action);
    this.rowActionEvent.emit(action);
  }

  //#endregion
}

