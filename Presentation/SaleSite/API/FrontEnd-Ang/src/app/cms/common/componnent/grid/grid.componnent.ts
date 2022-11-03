import { SelectionModel } from "@angular/cdk/collections";
import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output, ViewChild } from "@angular/core";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { Action } from "@ngrx/store";
import { ColumnProperties } from "../../constant/constant.common";

@Component({
  selector: 'app-grid',
  templateUrl: './grid.componnent.html',
  styleUrls: ['./grid.componnent.css']
})
export class GridComponnent implements OnInit {
  constructor(private cdr: ChangeDetectorRef) {
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
  actionEvent(action: Action) {
    //console.log(action);
    this.rowActionEvent.emit(action);
  }
  filteringDone(event) {
   //console.log(event);
  }
  reloadSorting($event) {
   //console.log($event);
  }
  toggleColumn($event) {
   //console.log($event);
  }
  ngAfterViewInit() {
    this.dataSource.sort = this.empTbSort;
  }
  //#endregion
}

