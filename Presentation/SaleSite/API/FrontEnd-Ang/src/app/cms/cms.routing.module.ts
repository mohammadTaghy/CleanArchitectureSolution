import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { LoginComponent } from "./auth/login.component";
import { LoginGuard } from "./auth/login.guard";
import { AdminPanelComponent } from "./main/AdminPanel/AdminPanel.component";
import { MainCmsPage } from "./main/main-cms-page.component";
import { PermissionsCmsPage } from "./main/modules/membership/permission/permission.component";
import { UserCmsPage } from "./main/modules/membership/user/user.component";

const routes: Routes = [
  {
    path: '', component: AdminPanelComponent,
    //canActivate: [LoginGuard],
    children: [
      { path: 'UserProfile', component: UserCmsPage },
      { path: 'Permissions', component: PermissionsCmsPage }
    ]
  },
  {
    path: 'auth', component: LoginComponent,
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CmsRoutingModule {

}
