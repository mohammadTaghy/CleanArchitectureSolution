import { Component, OnInit } from '@angular/core';
import { FlatTreeControl } from '@angular/cdk/tree'
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree'
import { Store } from '@ngrx/store';

import * as fromCmsApp from "../../store/cms.reducer"
import * as fromLoginAction from "../../auth/store/login.action"
import * as fromAdminPanelAction from "../../main/AdminPanel/store/adminPanel.action"
import { tap } from "rxjs/operators";
import { Router } from '@angular/router';
import { Membership_Permission } from '../../../model/membership_permission.model';
import { FeatureType } from '../../common/constant/constant.common';
//import { MaterialFlatTree } from '../../../commonComponent/componnent/material_flatTree';

interface IFlatNode {
  expandable: boolean;
  name: string;
  level: number;
}

@Component({
  selector: 'app-admin-panel',
  templateUrl:'./AdminPanel.component.html',
  
  styleUrls: ['./AdminPanel.component.scss']
})
export class AdminPanelComponent implements OnInit {
  constructor(private store: Store<fromCmsApp.CmsState<any>>, private router: Router) {
    
  }
  ngOnInit(): void {
    
    this.store.dispatch(new fromLoginAction.AutoLogin());
    this.store.dispatch(new fromAdminPanelAction.AdminPanelStartLoad());
    this.store.select("adminPanelState").subscribe(
      (data) => {
        console.log(FeatureType.Menu == 0);
        if (data.permissions != null)
          this.dataSource.data = data.permissions.filter(p => p.featureType == FeatureType.Menu || p.featureType == FeatureType.Form);
        console.log(this.dataSource.data);
      }
    )
  }
  private _transformer = (node: Membership_Permission, level: number) => {
    console.log("node");
    console.log(node);
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

  public isExpanded: boolean = false;
  public toggleMenu() {
    this.isExpanded = !this.isExpanded;
  }
}
