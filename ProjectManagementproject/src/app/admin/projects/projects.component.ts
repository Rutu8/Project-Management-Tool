import { Component, OnInit } from '@angular/core';
import { CommonService } from 'src/app/shared/common.service';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {
  projects:any;

  constructor(private api:CommonService){}


  ngOnInit(): void {
    this.api.get("api/projects").subscribe((result:any)=>{
      console.log(result);
      this.projects = result;

    })
  }

  delete(id:any){
    this.api.delete("api/projects/"+id).subscribe((result:any)=>{
      console.log(result);

    })
  }

}
