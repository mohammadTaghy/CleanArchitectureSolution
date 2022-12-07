import { SelectionModel } from "@angular/cdk/collections";
import { ChangeDetectorRef, Component, EventEmitter, HostListener, Input, OnInit, Output, ViewChild } from "@angular/core";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { MatSort, Sort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { Action } from "@ngrx/store";
import { ColumnProperties, CurrentState, IFilterData } from "../../constant/constant.common";
import { FilterDialogComponnent } from "./filter_dialog/filter_dialog.component";
import * as fromCmsAction from "../../../main/store/cms-module.action"

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
  //#region listener
  @HostListener('keydown', ['$event']) onKeydown(event: KeyboardEvent) {
    let newRow;
    console.log(event.key);
    // this key for edit
    if (event.key === 'Insert') {
      this.actionEvent(new fromCmsAction.ChangedView(CurrentState.Insert, null));
    }
    else if (event.key === '+') {
      console.log("+ clicked");
      this.onFilter("");
    }
  }
  //#endregion
  //#region property
  displayedColumns: string[];
  height = "400px";
  isPagerHidden = false;
  selectOptions = [10, 15, 20, 50];
  public selection: SelectionModel<ColumnProperties>;
  filterData: IFilterData[] = [];
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
  actionEvent(action: Action) {
    //console.log(action);
    this.rowActionEvent.emit(action);
  }

  //#endregion
}

