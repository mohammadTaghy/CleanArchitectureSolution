import { Component, EventEmitter, Input, NgZone, OnChanges, OnInit, Output, QueryList, SimpleChanges, ViewChild, ViewChildren } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import * as constant from "../../../constant/constant.common";
import { ConstantData } from "../../../constant/Constant_data.common";
import * as enumConstant from "../../../constant/enum.common";
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
    this.filterColumns = this.columns.map(p => new constant.DropdownDataType(p.header, "", p.field, false));
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
  filterTypes: constant.DropdownDataType[] = ConstantData.FilterType;
  filterColumns: constant.DropdownDataType[];
  joinType: constant.DropdownDataType[] = [
    { title: "و", value: enumConstant.JoinCondition.And, description: "and", disabled: false },
    { title: "یا", value: enumConstant.JoinCondition.Or, description: "or", disabled: false },
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
      joinCondition: enumConstant.JoinCondition.And,
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

