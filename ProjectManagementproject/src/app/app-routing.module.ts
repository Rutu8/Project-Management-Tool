import { LoginComponent } from './login/login.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminModule } from './admin/admin.module';

const routes: Routes = [
  {path:"", component:LoginComponent},
  {path:"login", component:LoginComponent},
  // {path:"login", component:LoginComponent},
  {path:"admin", loadChildren:()=>import('./admin/admin.module').then(m=>m.AdminModule)},
  {path:"user", loadChildren:()=>import('./user/user.module').then(u=>u.UserModule)}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
