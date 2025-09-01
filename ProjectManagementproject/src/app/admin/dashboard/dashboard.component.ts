import { HttpClient } from '@angular/common/http';
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
  constructor(private api:CommonService, private http:HttpClient){}
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

   download(){
  this.http.get("https://localhost:7171/api/Projects/excel", {observe:'response', responseType:'blob'}).subscribe((result:any)=>{
    console.log(result);
    this.api.common(result);
    })
}

downloadusers(){
  this.http.get("https://localhost:7171/api/users/excel", {observe:'response', responseType:'blob'}).subscribe((result:any)=>{
  this.api.common(result);
  })
}

downloadjobs(){
  this.http.get("https://localhost:7171/api/Projects/tasks/excel", {observe:'response', responseType:'blob'}).subscribe((result:any)=>{
  this.api.common(result);
  })
}

downloaduserjobs(){
  this.http.get("https://localhost:7171/api/Users/userjobs/excel", {observe:'response', responseType:'blob'}).subscribe((result:any)=>{
  this.api.common(result);
  })
}


}
