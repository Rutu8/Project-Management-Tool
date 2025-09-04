import { Component, OnInit } from '@angular/core';
import { CommonService } from 'src/app/shared/common.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {
  projects:any;

  constructor(private api:CommonService){}


  ngOnInit(): void {
    this.bind();

  }

  bind(){
      this.api.get("api/projects").subscribe((result:any)=>{
      console.log(result);
      this.projects = result;

    })
  }

  delete(id:any){
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
     this.api.delete("api/projects/"+id).subscribe((result:any)=>{
      console.log(result);
      if(result.success){
         Swal.fire({
      title: result.message,
      text: "result",
      icon: "success"
    });
      }else{
         Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: result.message,
            });
      }
    this.bind();

});
  }


    })
  }

}
