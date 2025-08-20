import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LandingComponent } from './landing/landing.component';
import { ProjectsComponent } from './projects/projects.component';
import { TasksComponent } from './tasks/tasks.component';
import { UsersComponent } from './users/users.component';
import { UserComponent } from './user/user.component';
import { ProjectComponent } from './project/project.component';
import { ProjectTasksComponent } from './project-tasks/project-tasks.component';

const routes: Routes = [
  {path:"", component:LandingComponent, children:[
    {path:"", component:DashboardComponent},
    {path:"projects", component:ProjectsComponent},
    {path:"tasks", component:TasksComponent},
    {path:"users", component:UsersComponent},
    {path:"user", component:UserComponent},
    {path:"user/:id", component:UserComponent},
    {path:"tasks/:projectId", component:TasksComponent},
    {path:"project", component:ProjectComponent},
    {path:"project/:id", component:ProjectComponent},
    {path:"project/:ownerId", component:ProjectComponent},
    {path:"projecttasks", component:ProjectTasksComponent},
    {path:"projecttasks/:userId", component:ProjectTasksComponent}


  ]

  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
