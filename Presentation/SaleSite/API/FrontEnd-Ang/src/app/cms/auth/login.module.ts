import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { LoginComponent } from './login.component';
import { CommonComponentModule } from '../../commonComponent/common.module';

@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonModule,
    FormsModule,
    //RouterModule.forChild([{ path: '', component: LoginComponent }]),
    CommonComponentModule
  ]
})
export class LoginModule { }
