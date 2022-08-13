import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  //{ path: '', loadChildren: () => import('./site/sites.module').then(m => m.SitesModule) },
  { path: '', redirectTo: '/admin', pathMatch: 'full' },
  { path: 'admin', loadChildren: () => import('./cms/cms.module').then(m => m.CmsModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
