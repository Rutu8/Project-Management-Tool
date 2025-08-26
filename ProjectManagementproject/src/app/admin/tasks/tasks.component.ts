import { Component, OnInit } from '@angular/core';
import { FormControl, FormControlName, FormGroup } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from 'src/app/shared/common.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
  projectId:any;
  tasks:any;
  formdata:any;
  userid:any;
  data:any;
  name:any;
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
      console.log(this.tasks);

      result.filter((n:any)=>{
        this.name=n.name

      })

    //  this.data= {id:0,title: this.tasks.title, status:this.tasks.status, projectId:this.projectId, priority:this.tasks.priority , userId:this.userid}

    })
    this.formdata = new FormGroup({
      id:new FormControl(""),
      name:new FormControl(""),
      title:new FormControl(""),
      status:new FormControl(""),
      projectId:new FormControl(""),
      priority:new FormControl("")
    })
  }

    update(data:any){
       this.formdata.patchValue({
          id:data.id,
          name:data.name,
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
         Swal.fire({
  position: "center",
  icon: "success",
  title: "Your Task has been Updated",
  showConfirmButton: false,
  timer: 1500
});
        this.bind();
      })

      }else{
        this.api.post("api/projects/tasks",data).subscribe((result:any)=>{
          console.log(result);
          Swal.fire({
  position: "center",
  icon: "success",
  title: "Your Task has been saved",
  showConfirmButton: false,
  timer: 1500
});
          this.bind();

        })
      }
    }

    // assign(taskid:any){
    //   this.api.post("api/projects/tasks", this.data).subscribe((result:any)=>{
    //     console.log(result);

    //   })

    // }

    delete(id:any){
      this.api.delete("api/projects/tasks/"+id).subscribe((result:any)=>{
        console.log(result);
        Swal.fire({
  title: "Are you sure?",
  text: "You won't be able to revert this!",
  icon: "warning",
  showCancelButton: true,
  confirmButtonColor: "#3085d6",
  cancelButtonColor: "#d33",
  confirmButtonText: "Yes, delete it!"
}).then((result) => {
  if (result.isConfirmed) {
    Swal.fire({
      title: "Deleted!",
      text: "Your Task has been deleted.",
      icon: "success"
    });
  }
});
        this.bind();


      })
    }



}
