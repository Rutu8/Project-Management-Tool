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
  constructor(private api:CommonService){}

  ngOnInit(): void {
   this.id = localStorage.getItem("User Id")

    this.api.get("api/projects/owner/"+this.id).subscribe((result:any)=>{
      this.projects = result.length;
      console.log(this.projects);

      console.log(result);

    })

  }

}
