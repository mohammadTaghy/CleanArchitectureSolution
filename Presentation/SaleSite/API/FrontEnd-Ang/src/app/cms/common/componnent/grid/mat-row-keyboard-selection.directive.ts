import { SelectionModel } from '@angular/cdk/collections';
import { Directive, ElementRef, Host, HostListener, Input, OnDestroy, OnInit, Self, AfterViewInit, Output, EventEmitter } from '@angular/core';
import { MatRow, MatTable, MatTableDataSource } from '@angular/material/table';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { CurrentState } from '../../constant/constant.common';
import { CmsContext } from '../../context/cms-context';
import * as fromCmsAction from "../../../main/store/cms-module.action"
import { Action } from '@ngrx/store';

@Directive({
  selector: '[matRowKeyboardSelection]'
})
export class MatRowKeyboardSelectionDirective implements OnInit, OnDestroy, AfterViewInit {


  private dataSource: MatTableDataSource<any>;
  private rows: NodeListOf<HTMLElement>;
  private renderedData: any[];

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

  @Output() rowActionEvent = new EventEmitter<Action>();

  private unsubscriber$ = new Subject();



  // @Host() private matTable: MatTable<any>
  // @Host() @Self() private row: MatRow
  constructor(private el: ElementRef,) { }

  ngOnInit(): void {

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

  ngOnDestroy(): void {
    this.unsubscriber$.next();
    this.unsubscriber$.complete();
  }

  @HostListener('focus', ['$event']) onFocus() {
    if (this.selectOnFocus && !this.selection.isMultipleSelection()) {
      this.selection.select(this.rowModel);
    }

    if (this.selectOnFocus && this.preventNewSelectionOnTab) {
     //console.log('tabIndex');
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

  @HostListener('keydown', ['$event']) onKeydown(event: KeyboardEvent) {
    let newRow;
    const currentIndex = this.renderedData.findIndex(row => row === this.rowModel);
   //console.log(event.key);
    if (event.key === 'ArrowDown') {
      newRow = this.rows[currentIndex + 1];
    } else if (event.key === 'ArrowUp') {
      newRow = this.rows[currentIndex - 1];
    } else if (event.key === 'Enter' || event.key === ' ') {
      if (this.toggleOnEnter) {
        this.selection.toggle(this.rowModel);
      }
      event.preventDefault();
    }
    else if (event.key === 'F2') {
      this.rowActionEvent.emit(new fromCmsAction.ChangedView(CurrentState.Edit, this.dataSource.data[currentIndex]["id"]));
    }
    else if (event.key === 'Insert') {
      this.rowActionEvent.emit(new fromCmsAction.ChangedView(CurrentState.Insert,null));
    }
    else if (event.key === 'Delete') {
      this.rowActionEvent.emit(new fromCmsAction.ChangedView(CurrentState.Delete, this.dataSource.data[currentIndex]["id"]));
    }
    else if (event.key === 'F4') {
      this.rowActionEvent.emit(new fromCmsAction.ChangedView(CurrentState.Details, this.dataSource.data[currentIndex]["id"]));
    }
    else if (event.key === 'Escape') {
      this.rowActionEvent.emit(new fromCmsAction.ChangedView(CurrentState.List, this.dataSource.data[currentIndex]["id"]));
    }
    if (newRow) {
      newRow.focus();
    }
    
  }

  private getTableRows() {
    let el = this.el.nativeElement;
    while (el && el.parentNode) {
      el = el.parentNode;
      if (el.tagName && el.tagName.toLowerCase() === 'mat-table' || el.hasAttribute('mat-table')) {
        return el.querySelectorAll('mat-row, tr[mat-row]');
      }
    }
    return null;
  }

}
