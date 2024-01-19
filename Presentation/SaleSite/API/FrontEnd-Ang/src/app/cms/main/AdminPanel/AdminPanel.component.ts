import { Component, OnInit } from '@angular/core';
import { FlatTreeControl } from '@angular/cdk/tree'
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree'
import { Store } from '@ngrx/store';

import * as fromCmsApp from "../../store/cms.reducer"
import * as fromLoginAction from "../../auth/store/login.action"
import * as fromAdminPanelAction from "../../main/AdminPanel/store/adminPanel.action"
import { filter, tap } from "rxjs/operators";
import { Router } from '@angular/router';
import { Membership_Permission } from '../../../model/membership/membership_permission.model';
import { FeatureType } from '../../common/constant/enum.common';
//import { MaterialFlatTree } from '../../../commonComponent/componnent/material_flatTree';

interface IFlatNode {
  expandable: boolean;
  name: string;
  level: number;
}

@Component({
  selector: 'app-admin-panel',
  templateUrl: './AdminPanel.component.html',

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
        //console.log(FeatureType.Menu == 0);
        //console.log(data.permissions);
        if (data.permissions != null)
          this.dataSource = this.filterPemissionForMenu(data.permissions);
        //console.log('adminPanelState');
        //console.log(this.dataSource);
      }
    )
  }
  //#region properties
  dataSource: Membership_Permission[];
  //#endregion
  //#region Methhod
  isExpanded: boolean = false;
  toggleMenu() {
    this.isExpanded = !this.isExpanded;
  }
  filterPemissionForMenu(permissions: Membership_Permission[]): Membership_Permission[] {
    let copyOfPermissions: Membership_Permission[]=[];
    permissions.filter(p => p.featureType == 0 || p.featureType == 1)
      .forEach((p) => {
        copyOfPermissions.push(
          {
            ...p,
            childList: this.filterPemissionForMenu(p.childList)
          });
      });
    return copyOfPermissions;
  }
  //#endregion
}
