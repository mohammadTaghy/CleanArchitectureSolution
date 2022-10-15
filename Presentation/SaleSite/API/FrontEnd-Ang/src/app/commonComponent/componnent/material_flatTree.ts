//import { Component, OnInit } from "@angular/core";
//import { FlatTreeControl } from '@angular/cdk/tree'
//import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree'

//export interface ITreeNode {
//  name: string;
//  ChildList?: ITreeNode[];
//}

//interface IFlatNode {
//  expandable: boolean;
//  name: string;
//  level: number;
//}
//@Component({
//  template: ''
//})
//export class MaterialFlatTree<T extends ITreeNode> implements OnInit  {

//  private _transformer = (node: T, level: number) => {
//    console.log(node);
//    return {
//      expandable: !!node.ChildList && node.ChildList.length > 0,
//      name: node.name,
//      level: level,
//    };
//  }

//  treeControl = new FlatTreeControl<IFlatNode>(
//    node => node.level, node => node.expandable);

//  treeFlattener = new MatTreeFlattener(
//    this._transformer, node => node.level, node => node.expandable, (node: T) => node.ChildList);

//  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

//  constructor(dataSource: T[]) {
//    this.dataSource.data = dataSource;
//    console.log(this.dataSource);
//    console.log(typeof (this.dataSource.data));
//  }

//  hasChild = (_: number, node: IFlatNode) => node.expandable;


//  ngOnInit(): void {
//  }

//}
