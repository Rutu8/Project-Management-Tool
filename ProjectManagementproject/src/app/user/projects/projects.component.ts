import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from 'src/app/shared/common.service';
import Swal from 'sweetalert2';

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
    this.bind();

  }
  bind(){
    this.formdata= new FormGroup({
      id: new FormControl(""),
      name:new FormControl("", Validators.compose([Validators.required])),
      description: new FormControl("", Validators.compose([Validators.required])),
      ownerId: new FormControl("", Validators.compose([Validators.required]))
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
        title: data.title,
        description: data.description,
        ownerId: data.ownerId
      })

  }

  submit(data:any){
    if(data.id != 0){
       this.api.put("api/projects/"+data.id, data).subscribe((result:any)=>{
        console.log(data.id);

        console.log(result);
        Swal.fire({
  position: "top-end",
  icon: "success",
  title: "Your Project has been Updated",
  showConfirmButton: false,
  timer: 1500
});
this.bind();
      })
    }else{
       this.api.post("api/projects", data).subscribe((result:any)=>{
      console.log(result);
      Swal.fire({
  position: "center",
  icon: "success",
  title: "Your Project has been saved",
  showConfirmButton: false,
  timer: 1500
});
this.bind();
    })

    }





  }

  get f(){
    return this.formdata.controls;
  }

}
