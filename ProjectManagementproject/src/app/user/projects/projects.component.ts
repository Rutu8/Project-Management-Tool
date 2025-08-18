import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from 'src/app/shared/common.service';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {
  formdata:any;
  id:any;
  project:any;
  constructor(private api:CommonService, private route:ActivatedRoute){}

  ngOnInit(): void {
    this.formdata= new FormGroup({
      id: new FormControl(""),
      name:new FormControl(""),
      description: new FormControl(""),
      ownerId: new FormControl("")
    })

    // this.id = JSON.parse(localStorage.getItem("User Id") || "[]");
    this.id = localStorage.getItem("User Id")
    this.api.get("api/projects/owner/"+this.id).subscribe((result:any)=>{
      this.project = result;

      console.log(result);
    })
  }


  update(data:any){
    this.formdata.patchValue({
        id: data.id,
        name: data.name,
        description: data.description,
        ownerId: data.ownerId
      })

  }

  submit(data:any){
    if(data.id != 0){
       this.api.put("api/projects/"+data.id, data).subscribe((result:any)=>{
        console.log(result);
      })
    }else{
       this.api.post("api/projects", data).subscribe((result:any)=>{
      console.log(result);
    })

    }



  }

}
