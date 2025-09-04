import { LoginComponent } from './login/login.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminModule } from './admin/admin.module';
import { authGuard } from './auth.guard';

const routes: Routes = [
  {path:"", component:LoginComponent},
  {path:"login", component:LoginComponent},
  // {path:"login", component:LoginComponent},
  {path:"admin", loadChildren:()=>import('./admin/admin.module').then(m=>m.AdminModule),
    canActivate:[authGuard],
    data:{expectedtype: 'Admin'}
  },
  {path:"user", loadChildren:()=>import('./user/user.module').then(u=>u.UserModule),
    canActivate:[authGuard],
    data:{expectedtype:'User'}
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
