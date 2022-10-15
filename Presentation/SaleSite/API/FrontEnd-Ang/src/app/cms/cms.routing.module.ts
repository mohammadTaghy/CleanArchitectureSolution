import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { LoginComponent } from "./auth/login.component";
import { LoginGuard } from "./auth/login.guard";
import { AdminPanelComponent } from "./main/AdminPanel/AdminPanel.component";
import { MainCmsPage } from "./main/main-cms-page.component";
import { UserCmsPage } from "./main/modules/membership/user/user.component";

const routes: Routes = [
  {
    path: '', component: AdminPanelComponent,
    //canActivate: [LoginGuard],
    children: [
      { path: 'membership_user', component: UserCmsPage }
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
