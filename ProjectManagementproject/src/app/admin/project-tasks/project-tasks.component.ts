import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from 'src/app/shared/common.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-project-tasks',
  templateUrl: './project-tasks.component.html',
  styleUrls: ['./project-tasks.component.css']
})
export class ProjectTasksComponent implements OnInit {
  usertask:any;
  userid:any;
  user:any;
  alltasks:any;
  project:any;
  projectid:any;

  constructor(private api:CommonService, private route:ActivatedRoute){
    this.userid = this.route.snapshot.paramMap.get("userId");
    // console.log(this.userid);

  }

  ngOnInit(): void {
    this.api.get("api/users/"+ this.userid).subscribe((result:any)=>{
      this.user=result;
    })
    this.bind();

    // this.api.get("api/projects/owner/" +this.userid ).subscribe((result:any)=>{
    //   console.log(result);

    // })




  }

  bind(){
    this.api.get("api/users/jobs/"+ this.userid).subscribe((result:any)=>{
      this.usertask = result;
      console.log(this.usertask);
      this.project = this.usertask.filter((projectId:any)=>{
        this.projectid = projectId.projectId;
      })

      this.api.get("api/projects/tasks").subscribe((alltasks:any)=>{
        this.alltasks = alltasks.filter((task:any)=>{
          let found = false;
        // console.log(task);

          this.usertask.forEach((t:any) => {
            if(t.job.id == task.id){
              found = true;
            }
          });
          return !found;

        });


      });

    });






  }

  assign(jobId:number){
    let data = {id:0, userId:this.userid, jobId:jobId, projectId: this.projectid}
    this.api.post("api/users/assigntask", data).subscribe((result:any)=>{
      console.log(result);
      this.bind();
    })


  }

  unassign(jobId:any){
    this.api.delete("api/users/unassigned/"+ this.userid + "/" + jobId).subscribe((result:any)=>{
      console.log(result);
      Swal.fire({
  title: "Are you sure?",
  // text: "You won't be able to revert this!",
  icon: "warning",
  showCancelButton: true,
  confirmButtonColor: "#3085d6",
  cancelButtonColor: "#d33",
  confirmButtonText: "Yes, delete it!"
}).then((result) => {
  if (result.isConfirmed) {
    Swal.fire({
      title: "Deleted!",
      text: "Your task has been unassigned.",
      icon: "success"
    });
  }
});
      this.bind();

    })

  }

}
