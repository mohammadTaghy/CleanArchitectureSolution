import { FlatTreeControl } from "@angular/cdk/tree";
import { Component, Input, OnChanges, OnInit, SimpleChanges } from "@angular/core";
import { MatTreeFlatDataSource, MatTreeFlattener } from "@angular/material/tree";
import { FeatureType } from "../../constant/constant.common";


@Component({
  selector: 'app-tree',
  templateUrl: './tree.component.html',
  styleUrls: ['./tree.component.css']
})
export class TreeComponent implements OnInit, OnChanges {
  ngOnInit(): void {
    //this.dataSource.data = this.inputData;
  }
  constructor() {

  }
  ngOnChanges(changes: SimpleChanges): void {
    //console.log(changes);
    //console.log(this.inputData);

    this.dataSource.data = this.inputData;
  }
  //#region input
  @Input() inputData: any;
  //#endregion

  private _transformer = (node: any, level: number) => {
    //console.log("node");
    //console.log(node);
    return {
      expandable: node.featureType == FeatureType.Menu,
      name: node.name,
      level: level,
      title: node.title
    };
  }

  treeControl = new FlatTreeControl<IFlatNode>(
    node => node.level, node => node.expandable);

  treeFlattener = new MatTreeFlattener(
    this._transformer, node => node.level, node => node.expandable, node => node.childList);

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  hasChild = (_: number, node: IFlatNode) => node.expandable;

}
interface IFlatNode {
  expandable: boolean;
  name: string;
  level: number;
}
