import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { CommonService } from 'src/app/shared/common.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  projects:any
  id:any;
  jobs:any;
  chart:any;
  constructor(private api:CommonService, private http:HttpClient){}

  ngOnInit(): void {
   this.id = localStorage.getItem("User Id")

    this.api.get("api/projects/owner/"+this.id).subscribe((result:any)=>{
      this.projects = result.length;
      // console.log(this.projects);

      // console.log(result);

    })

     this.api.get("api/users/jobs/"+this.id).subscribe((result:any)=>{
      // console.log(result);
      this.jobs= result.length;

    })


  }

 


}
