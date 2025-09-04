import { Component, OnInit } from '@angular/core';
import { FormControl,FormControlName, FormGroup,Validators,} from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from 'src/app/shared/common.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css'],
})
export class TasksComponent implements OnInit {
  projectId: any;
  tasks: any;
  formdata: any;
  userid: any;
  data: any;
  name:any;

  constructor(private route: ActivatedRoute, private api: CommonService) {

  }
  ngOnInit(): void {
       this.projectId = this.route.snapshot.paramMap.get('projectId');
    console.log(this.projectId);
    this.bind();

  }

  bind() {
    this.formdata = new FormGroup({
      id: new FormControl('0'),
      name: new FormControl(this.name, Validators.compose([Validators.required])),
      title: new FormControl('', Validators.compose([Validators.required])),
      status: new FormControl('', Validators.compose([Validators.required])),
      projectId: new FormControl(this.projectId, Validators.compose([Validators.required])),
      priority: new FormControl('', Validators.compose([Validators.required])),
    });

     this.api.get('api/projects/tasks/' + this.projectId).subscribe((result: any) => {
        console.log(result);
        this.tasks = result;
       this.name = this.tasks[0].name;
        console.log(this.tasks[0].name);
      });

  }

  update(data: any) {
    this.formdata.patchValue({
      id: data.id,
      name: data.name,
      title: data.title,
      status: data.status,
      projectId: data.projectId,
      priority: data.priority,

    });
    console.log(data);
  }

  submit(data: any) {
    if (this.formdata.invalid) {
      this.formdata.markAllAsTouched();
      Swal.fire({
        icon: 'error',
        title: 'Form Incomplete',
        text: 'Please fill in all required fields correctly.',
      });
      return;
    }
    if (data.id != 0) {
      this.api.put('api/projects/tasks/' + data.id, data).subscribe((result: any) => {
          console.log(result);
          Swal.fire({
            position: 'center',
            icon: 'success',
            title: 'Your Task has been Updated',
            showConfirmButton: false,
            timer: 1500,
          });
          this.bind();
        });
    } else {
      this.api.post('api/projects/tasks', data).subscribe((result: any) => {
        console.log(result);
        Swal.fire({
          position: 'center',
          icon: 'success',
          title: 'Your Task has been saved',
          showConfirmButton: false,
          timer: 1500,
        });
        this.bind();
      });
    }
  }

  // assign(taskid:any){
  //   this.api.post("api/projects/tasks", this.data).subscribe((result:any)=>{
  //     console.log(result);

  //   })

  // }

  delete(id: any) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.isConfirmed) {
        this.api.delete('api/projects/tasks/' + id).subscribe((result: any) => {
          console.log(result);
          if (result.success) {
            Swal.fire({
              title: 'Deleted!',
              text: 'Your Task has been deleted.',
              icon: 'success',
            });
          } else {
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: result.message,
            });
          }
          this.bind();
        });
      }
    });
  }
}
