import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { LoginComponent } from "./auth/login.component";
import { LoginGuard } from "./auth/login.guard";
import { AdminPanelComponent } from "./main/AdminPanel.component";

const routes: Routes = [
  {
    path: '', component: AdminPanelComponent,
    canActivate: [LoginGuard]
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
