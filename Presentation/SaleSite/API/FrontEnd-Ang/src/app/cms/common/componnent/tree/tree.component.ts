import { SelectionModel } from "@angular/cdk/collections";
import { FlatTreeControl } from "@angular/cdk/tree";
import { Component, ElementRef, EventEmitter, HostListener, Input, OnChanges, OnInit, Output, SimpleChanges } from "@angular/core";
import { MatTree, MatTreeFlatDataSource, MatTreeFlattener, MatTreeNode, MatTreeNodeDef } from "@angular/material/tree";
import { Action } from "@ngrx/store";
import { CurrentState, FeatureType } from "../../constant/enum.common";
import * as fromCmsAction from "../../../main/store/cms-module.action"

@Component({
  selector: 'app-tree',
  templateUrl: './tree.component.html',
  styleUrls: ['./tree.component.css']
})
export class TreeComponent implements OnInit, OnChanges {
  //#region implement
  ngOnInit(): void {
    //this.dataSource.data = this.inputData;
  }
  constructor(private el: ElementRef) {
    this.selection = new SelectionModel<any>(false);
   //console.log("tree Constructor");
   //console.log(this);
   //console.log(this.inputData);
  }
  ngOnChanges(changes: SimpleChanges): void {
   //console.log("changes");
   //console.log(changes);
    if (changes['inputData'] != undefined) {
     //console.log("inputData");
     //console.log(this.inputData);
      this.dataSource.data = this.inputData;
     //console.log(this.dataSource.data);
    }
  }
  //#endregion
  //#region input
  @Input() inputData: any;
  @Input() hasCheckBox: boolean;
  @Input() hasRadio: boolean;
  @Input() hasHref: boolean;
  @Input() treeName: boolean;
  @Input() hasManipilateButton: boolean;
  //#endregion
  //#region output
  @Output() rowActionEvent = new EventEmitter<Action>();
  @Output() selectedItemEvent = new EventEmitter<any>();

  //#endregion
  //#region properties
  oldSelectedNode: any;
  selectedNode: any;
  selection: SelectionModel<any>;
  _transformer = (node: any, level: number) => {
  //console.log("node");
  //console.log(node);
    return {
      ...node,
      expandable: node.childList != null && node.childList.length>0,
      name: node.name,
      level: level,
      title: node.title,
    };
  }
  treeControl = new FlatTreeControl<any>(
    node => node.level, node => node.expandable);

  treeFlattener = new MatTreeFlattener(
    this._transformer, node => node.level, node => node.expandable, node => node.childList);

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  hasChild = (_: number, node: any) => node.expandable;
  //#endregion
  
  //#region private Method
  addNewItem() {
   //console.log("add new Item");
    this.actionEvent(new fromCmsAction.ChangedView(CurrentState.Insert, this.selectedNode));
  }
  deleteItem() {
    //console.log(this.dataSource.data[this.selectedId]);
    this.actionEvent(new fromCmsAction.ChangedView(CurrentState.Delete, this.selectedNode));
  }
  editItem() {
    //console.log(this.dataSource.data);
    //console.log(this.selectedId);
    //console.log(this.dataSource.data.find(p => p.id == this.selectedId));
    console.log(this.selectedNode);
    this.actionEvent(new fromCmsAction.ChangedView(CurrentState.Edit, this.selectedNode));
  }
  nodeClicked(node) {

    this.selectedNode = node;
    this.selectedItemEvent.emit(this.selectedNode);
   console.log(this.selectedNode);
  }
  //#endregion
  //#region event
  actionEvent(action: Action) {
    //console.log(action);
    this.rowActionEvent.emit(action);
  }
  //#endregion
}
