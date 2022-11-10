import { SelectionModel } from "@angular/cdk/collections";
import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output, ViewChild } from "@angular/core";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { MatSort, Sort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { Action } from "@ngrx/store";
import { ColumnProperties } from "../../constant/constant.common";
import { FilterDialogComponnent } from "./filter_dialog/filter_dialog.component";

@Component({
  selector: 'app-grid',
  templateUrl: './grid.componnent.html',
  styleUrls: ['./grid.componnent.css']
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
  //#endregion
  //#region property
  displayedColumns: string[];
  height = "400px";
  isPagerHidden = false;
  selectOptions = [10, 15, 20, 50];
  public selection: SelectionModel<ColumnProperties>;

  //#endregion
  //#region event
  onFilter(event: any) {
    console.log("open dialog");
    const dialogRef = this.dialog.open(FilterDialogComponnent);
    dialogRef.componentInstance.columns = this.columns.filter(p => p.isGridVisible);
    //this.dialog.closeAll();
    //dialogRef.componentInstance.closeDialog.subscribe(() => {
    //  console.log("after emit close");
    //  dialogRef.close(true);
    //  this.dialog.closeAll();
    //})
    //dialogRef.afterClosed().subscribe(result => {
    //  console.log('The dialog was closed');
    //  console.log(result);
    //});
  }
  actionEvent(action: Action) {
    //console.log(action);
    this.rowActionEvent.emit(action);
  }

  //#endregion
}

