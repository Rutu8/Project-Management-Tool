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
    this.api.get("api/projects").subscribe((result:any)=>{
      console.log(result);
      this.projects = result;

    })
  }

  delete(id:any){
    this.api.delete("api/projects/"+id).subscribe((result:any)=>{
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
      text: "Project has been deleted.",
      icon: "success"
    });
  }
});

    })
  }

}
