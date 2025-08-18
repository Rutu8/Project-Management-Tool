import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { CommonService } from 'src/app/shared/common.service';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit {
  formdata:any;
  id:any;
  ownerId:any;
  constructor(private api:CommonService, private router:Router, private route:ActivatedRoute){
    this.id= this.route.snapshot.paramMap.get("id");
    this.ownerId = this.route.snapshot.paramMap.get("ownerId");
    // this.ownerId = this.route.snapshot.params['ownerId'];

    console.log(this.ownerId);



  }


  ngOnInit(): void {
    this.formdata = new FormGroup({
      id: new FormControl(""),
      name: new FormControl(""),
      description: new FormControl(""),
      ownerId: new FormControl("")
    })
    this.api.get("api/projects/project/"+ this.id).subscribe((result:any)=>{
      this.formdata.patchValue({
        id:result.id,
        name:result.name,
        description:result.description,
        ownerId:result.ownerId
      })
    })

    this.api.get("api/projects/owner/"+ this.ownerId).subscribe((result:any)=>{
      console.log(result);
      result.filter((ownerid:any)=>{
        this.api.get("api/users"+ownerid.ownerId).subscribe((result:any)=>{
          // ownerid = result.id;
          console.log(result);

        })
      })
    })
  }

  submit(data:any){
    console.log(data);
    this.api.put("api/projects/"+this.id,data).subscribe((result:any)=>{
        console.log(result);
    })

    // this.api.post("api/projects/", data).subscribe((result:any)=>{
    //   console.log(result);

    // })

  }

}
