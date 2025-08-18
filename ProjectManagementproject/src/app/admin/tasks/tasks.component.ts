import { Component, OnInit } from '@angular/core';
import { FormControl, FormControlName, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from 'src/app/shared/common.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
  projectId:any;
  tasks:any;
  formdata:any;
  constructor(private route:ActivatedRoute, private api:CommonService){
    this.projectId = this.route.snapshot.paramMap.get("projectId");
    console.log(this.projectId);


  }
  ngOnInit(): void {
    this.bind();



  }

  bind(){
     this.api.get("api/projects/tasks/"+this.projectId).subscribe((result:any)=>{
      console.log(result);
      this.tasks=result;
    })
    this.formdata = new FormGroup({
      id:new FormControl(""),
      title:new FormControl(""),
      status:new FormControl(""),
      projectId:new FormControl(""),
      priority:new FormControl("")
    })
  }

    update(data:any){
       this.formdata.patchValue({
          id:data.id,
      title:data.title,
      status:data.status,
      projectId:data.projectId,
      priority:data.priority
        })
        console.log(data);
    }

    submit(data:any){
      if(data.id !=0){
        this.api.put("api/projects/tasks/"+ data.id,data).subscribe((result:any)=>{
        console.log(result);
        this.bind();
      })

      }else{
        this.api.post("api/projects/tasks",data).subscribe((result:any)=>{
          console.log(result);
          this.bind();

        })
      }


    }

    delete(id:any){
      this.api.delete("api/projects/tasks/"+id).subscribe((result:any)=>{
        console.log(result);
        this.bind();

      })
    }



}
