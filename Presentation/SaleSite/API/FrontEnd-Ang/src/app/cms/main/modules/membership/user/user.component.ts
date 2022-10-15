import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Store } from "@ngrx/store";
import { ApiAddresses, ApiUrlPostfix } from "../../../../../commonComponent/apiAddresses/apiAddresses.common";

import { Membership_User } from "../../../../../model/membership_user.model";
import * as fromCmsApp from "../../../../store/cms.reducer"
import * as fromCmsAction from "../../../store/cms-module.action"

@Component({
  selector: 'membership-user-cms',
  templateUrl: './user.component.html'
})
export class UserCmsPage implements OnInit {
  ngOnInit(): void {
    console.log("user");
  }
  constructor(private store: Store<fromCmsApp.CmsState<Membership_User>>, private router: Router, private apiAddresses: ApiAddresses) {
    store.dispatch(new fromCmsAction.FetchData(null, apiAddresses.GetServiceUrl(ApiUrlPostfix.MembershipUsers), "Get"));
  }
}
