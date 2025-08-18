import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LandingComponent } from './landing/landing.component';
import { ProjectsComponent } from './projects/projects.component';
import { TasksComponent } from './tasks/tasks.component';

const routes: Routes = [
  {path:"", component:LandingComponent, children:[
    {path:"", component:DashboardComponent},
    // {path:"dashboard", component:DashboardComponent},
    {path:"home", component:HomeComponent},
    {path:"projects", component:ProjectsComponent},
    {path:"tasks", component:TasksComponent}
  ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
