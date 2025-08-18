import { Component, OnInit } from '@angular/core';
import { CommonService } from 'src/app/shared/common.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
Users:any;
projects:any
tasks:any;
  constructor(private api:CommonService){}
  ngOnInit(): void {
    this.api.get("api/users").subscribe((result:any)=>{
      this.Users = result.length;
      console.log(result);

    })

    this.api.get("api/projects").subscribe((result:any)=>{
      console.log(result);
      this.projects = result.length;

    })

    this.api.get("api/projects/tasks").subscribe((result:any)=>{
      console.log(result);
      this.tasks = result.length;


    })

  }

}
