import { Component, EventEmitter, Input, NgZone, OnChanges, OnInit, Output, SimpleChanges } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import * as constant from "../../../constant/constant.common";
import { ColumnProperties } from "../../../constant/constant.common";

@Component({
  selector: 'app-filter_dialog',
  templateUrl: './filter_dialog.component.html',

  styleUrls: ['./filter_dialog.component.css']
})
export class FilterDialogComponnent implements OnInit, OnChanges {
  constructor(
    public dialogRef: MatDialogRef<FilterDialogComponnent>, public ngZone: NgZone
  ) {

  }
  //#region implement
  ngOnChanges(changes: SimpleChanges): void {

  }
  ngOnInit(): void {
    this.filterColumns = this.columns.map(p => new constant.dropdownDataType(p.header, "", p.field, false));
    this.dialogRef.disableClose = true;

    this.addFilterRow();
  }
  //#endregion
  //#region properties
  filters: DialogData[] = [];
  columnTitle = "نام ستون";
  columnName = "columnName";
  columnWidth = "w-100";
  filterName = "filterName";
  filterTitle = "نوع شرط";
  filterWidth = "w-100";
  filterValueName = "filterValueName";
  filterValueWidth = "w-100";
  placeHolder = "مقدار فیلتر";
  filterTypes: constant.dropdownDataType[] = [
    { title: "شامل", value: 0, description: "like '%__%'", disabled: false },
    { title: "شروع شود با", value: 1, description: "like '__%'", disabled: false },
    { title: "خاتمه یابد با", value: 2, description: "like '%__'", disabled: false },
    { title: "برابر", value: 3, description: "==", disabled: false },
    { title: "بزرگتر از", value: 4, description: ">", disabled: false },
    { title: "کوچکتر از", value: 5, description: "<", disabled: false },
    { title: "بزرگتر و مساوی از", value: 6, description: ">=", disabled: false },
    { title: "کوچکتر و مساوی از", value: 7, description: "<=", disabled: false },
  ];
  filterColumns: constant.dropdownDataType[];
  //#endregion
  //#region input
  public columns: ColumnProperties[];
  @Input() selectedData: any;

  //#endregion
  //#region output
  @Output() closeDialog = new EventEmitter<any>();
  //#endregion
  //#region method

  onNoClick(): void {
    this.ngZone.run(() => {
      console.log("close");
      this.dialogRef.close(true);
      this.closeDialog.emit();

    });
  }
  addFilterRow(): void {
    this.filters.push({
      selectedColumn: this.filterColumns[0].value,
      selectedFilterType: this.filterTypes[0].value as number,
      value: ""
    });
  }
  onOkCkick(): void {

    console.log(this.filters);
    this.closeDialog.emit();
    this.dialogRef.close();
  }
  //#endregion
}
export interface DialogData {
  selectedColumn: string | number;
  selectedFilterType: number;
  value: string;
}
