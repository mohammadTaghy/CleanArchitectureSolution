import { SelectionModel } from '@angular/cdk/collections';
import { Directive, ElementRef, Host, HostListener, Input, OnDestroy, OnInit, Self, AfterViewInit, Output, EventEmitter, Inject } from '@angular/core';
import { MatRow, MatTable, MatTableDataSource } from '@angular/material/table';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { CurrentState } from '../constant/enum.common';
import { CmsContext } from '../context/cms-context';
import * as fromCmsAction from "../../main/store/cms-module.action"
import { Action } from '@ngrx/store';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FilterDialogComponnent } from './grid/filter_dialog/filter_dialog.component';

@Directive({
  selector: '[matRowKeyboardSelection]'
})
export class MatRowKeyboardSelectionDirective implements OnInit, OnDestroy, AfterViewInit {
  constructor(private el: ElementRef,) { }
  //#region private property
  private dataSource: MatTableDataSource<any>;
  private rows: NodeListOf<HTMLElement>;
  private renderedData: any[];
  private unsubscriber$ = new Subject();
  //#endregion
  //#region Input
  @Input('matRowKeyboardSelection') set MatRowKeyboardSelection(selection) {
    this.selection = selection;
  }
  @Input() selection: SelectionModel<any>;
  @Input() rowModel;
  @Input() toggleOnEnter = true;
  @Input() selectOnFocus = true;
  @Input() deselectOnBlur = false;
  @Input() preventNewSelectionOnTab = false;
  @Input() matTable: MatTable<any>;
  @Input() row: MatRow;
  //#endregion
  //#region output
  @Output() rowActionEvent = new EventEmitter<Action>();
  @Output() selectedRowIdEvent = new EventEmitter<number>();
  @Output() openFilterDialog = new EventEmitter<any>();
  //#endregion
  //#region implement
  ngOnInit(): void {

  }
  ngOnDestroy(): void {
    this.unsubscriber$.next(null);
    this.unsubscriber$.complete();
  }
  ngAfterViewInit() {
    // if (!this.selection) {
    //     throw new Error('Attribute \'selection\' is required');
    //   }
    if (!this.matTable || !this.matTable.dataSource) {
      throw new Error('MatTable [dataSource] is required');
    }
    if (!this.rowModel) {
      throw new Error('[rowModel] is required');
    }
    if (this.el.nativeElement.tabIndex < 0) {
      this.el.nativeElement.tabIndex = 0;
    }
    this.dataSource = this.matTable.dataSource as MatTableDataSource<any>;
    this.dataSource.connect().pipe(takeUntil(this.unsubscriber$)).subscribe(data => {
      this.renderedData = data;
      //console.log(this.renderedData);
      this.rows = this.getTableRows();
      //console.log(this.rows);
    });
  }
  //#endregion
  //#region listener
  @HostListener('focus', ['$event']) onFocus() {
    if (this.selectOnFocus && !this.selection.isMultipleSelection()) {
      this.selection.select(this.rowModel);
    }

    if (this.selectOnFocus && this.preventNewSelectionOnTab) {
      this.rows.forEach(row => {
        if (row !== this.el.nativeElement) {
          row.tabIndex = -1;
        }
      });
    }
  }

  @HostListener('blur', ['$event']) onBlur() {
    if (this.deselectOnBlur && !this.selection.isMultipleSelection()) {
      this.selection.deselect(this.rowModel);
    }
    if (this.selectOnFocus) {
      this.el.nativeElement.tabIndex = 0;
    }
  }
  @HostListener('click', ['$event']) onClick(event: MouseEvent) {
    this.selectedRow(event);
    event.preventDefault();
  }
  @HostListener('keydown', ['$event']) onKeydown(event: KeyboardEvent) {
    let newRow;
    const currentIndex = this.renderedData.findIndex(row => row === this.rowModel);
    console.log(event.key);
    if (event.key === 'ArrowDown') {
      newRow = this.rows[currentIndex + 1];
    } else if (event.key === 'ArrowUp') {
      newRow = this.rows[currentIndex - 1];
    } else if (event.key === 'Enter' || event.key === ' ') {
      this.selectedRow(event)
    }
    // this key for edit
    else if (event.key === 'F2') {
      if (this.rowModel != undefined || this.rowModel != null)
        this.rowActionEvent.emit(new fromCmsAction.ChangedView(CurrentState.Edit, this.dataSource.data[currentIndex]));
    }
    else if (event.key === 'Insert') {
      this.rowActionEvent.emit(new fromCmsAction.ChangedView(CurrentState.Insert, null));
    }
    else if (event.key === 'Delete') {
      if (this.rowModel != undefined || this.rowModel != null)
        this.rowActionEvent.emit(new fromCmsAction.ChangedView(CurrentState.Delete, this.dataSource.data[currentIndex]));
    }
    // this key for view
    else if (event.key === 'F4') {
      if (this.rowModel != undefined || this.rowModel != null)
        this.rowActionEvent.emit(new fromCmsAction.ChangedView(CurrentState.Details, this.dataSource.data[currentIndex]));
    }
    else if (event.key === 'Escape') {
      this.rowActionEvent.emit(new fromCmsAction.ChangedView(CurrentState.List, this.dataSource.data[currentIndex]));
    }
    else if (event.key === '+') {
      console.log("+ clicked");
      this.openFilterDialog.emit(this.selectedRow);
    }
    if (newRow) {
      newRow.focus();

    }

  }
  //#endregion
  //#region private Method
  selectedRow(event) {
    this.unSelectRows();
    if (this.toggleOnEnter) {
      this.selection.toggle(this.rowModel);
      //console.log(this.rowModel);
      //console.log(this.rowModel["id"]);
      this.selectedRowIdEvent.emit(this.rowModel);
    }
    event.preventDefault();
  }
  
  unSelectRows() {
    this.selection.clear();
    
  }
  getTableRows() {
    let el = this.el.nativeElement;
    while (el && el.parentNode) {
      el = el.parentNode;
      if (el.tagName && el.tagName.toLowerCase() === 'mat-table' || el.hasAttribute('mat-table')) {
        return el.querySelectorAll('mat-row, tr[mat-row]');
      }
    }
    return null;
  }

  //#endregion
}

