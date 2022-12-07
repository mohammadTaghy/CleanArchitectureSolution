import { Dialog } from "@angular/cdk/dialog";
import { Component, EventEmitter, Input, NgZone, OnChanges, OnInit, Output, QueryList, SimpleChanges, ViewChild, ViewChildren } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import * as constant from "../../../constant/constant.common";
import { BaseUIComponent } from "../../baseUI.compnent";
import { DropdownComponnent } from "../../dropdown/dropdown.component";
import { TextboxComponnent } from "../../textbox/textbox.component";

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
  @ViewChildren(TextboxComponnent) textboxChildren: QueryList<TextboxComponnent>;
  @ViewChildren(DropdownComponnent) dropDownChildren: QueryList<DropdownComponnent>;

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
  
  columnTitle = "نام ستون";
  columnName = "columnName";
  columnWidth = "col s3 m3";
  filterName = "filterName";
  filterTitle = "نوع شرط";
  filterWidth = "col s4 m4";
  filterValueName = "filterValueName";
  filterValueWidth = "col s4 m4";
  placeHolder = "مقدار فیلتر";
  joinConditionName = "joinName";
  joinConditionTitle = "";
  joinConditionWidth = "col s1 m1";
  filterTypes: constant.dropdownDataType[] = [
    { title: "شامل", value: constant.FilterType.Like, description: "like '%__%'", disabled: false },
    { title: "شروع شود با", value: constant.FilterType.StartWith, description: "like '__%'", disabled: false },
    { title: "خاتمه یابد با", value: constant.FilterType.EndWith, description: "like '%__'", disabled: false },
    { title: "برابر", value: constant.FilterType.Equal, description: "==", disabled: false },
    { title: "بزرگتر از", value: constant.FilterType.GreaterThan, description: ">", disabled: false },
    { title: "کوچکتر از", value: constant.FilterType.LessThan, description: "<", disabled: false },
    { title: "بزرگتر و مساوی از", value: constant.FilterType.GreaterOrEqual, description: ">=", disabled: false },
    { title: "کوچکتر و مساوی از", value: constant.FilterType.LessOrEqual, description: "<=", disabled: false },
  ];
  filterColumns: constant.dropdownDataType[];
  joinType: constant.dropdownDataType[] = [
    { title: "و", value: constant.JoinCondition.And, description: "and", disabled: false },
    { title: "یا", value: constant.JoinCondition.Or, description: "or", disabled: false },
  ];
  tempValue: string="test";
  //#endregion
  //#region input
  public columns: constant.ColumnProperties[];
  @Input() selectedData: any;
  @Input() filters: constant.IFilterData[]=[];
  @Input() sorts: constant.ISortData[];
  //#endregion
  //#region output
  @Output() filterDataResults = new EventEmitter<constant.IFilterData[]>();
  //#endregion
  //#region method

  onNoClick(): void {
   
     //console.log("close");
      this.dialogRef.close(true);

  }
  addFilterRow(): void {
    this.filters = [...this.filters, {
      selectedColumn: this.filterColumns[0].value,
      selectedFilterType: this.filterTypes[0].value as number,
      value: "",
      name: "filterBox" + this.filters.length,
      joinCondition: constant.JoinCondition.And,
    }];
  }
  removeFilterRow(filterItem: constant.IFilterData) {
    let index: number = this.filters.indexOf(filterItem);
    console.log("index:" + index);
    this.filters = this.filters.filter(p => p != filterItem);
  }
  onOkCkick(): void {
   //console.log(this.textboxChildren);
   //console.log(this.dropDownChildren);
    let results: constant.IFilterData[]=[];
    this.textboxChildren.map(p => {
     //console.log(this.dropDownChildren.find(q => q.name == p.name + this.columnName));
      results.push({
        value: p.inputValue,
        selectedColumn: this.dropDownChildren.find(q => q.name == p.name + this.columnName).inputValue,
        selectedFilterType: +this.dropDownChildren.find(q => q.name == p.name + this.filterName).inputValue,
        name: p.name,
        joinCondition: +this.dropDownChildren.find(q => q.name == p.name + this.joinConditionName).inputValue
      });
    });
    this.filterDataResults.emit(results);
   //console.log(results);

    this.dialogRef.close();

  }
  //#endregion
}

