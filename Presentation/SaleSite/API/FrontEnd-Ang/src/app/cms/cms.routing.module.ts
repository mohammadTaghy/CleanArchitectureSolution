import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { LoginComponent } from "./auth/login.component";
import { LoginGuard } from "./auth/login.guard";
import { MainPageComponent } from "./main/main-page.component";

const routes: Routes = [
  {
    path: '', component: MainPageComponent,
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
