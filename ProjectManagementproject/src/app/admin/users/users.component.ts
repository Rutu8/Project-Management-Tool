import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonService } from 'src/app/shared/common.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  users:any;
  constructor(private api:CommonService, private router:Router){}


  ngOnInit(): void {
    this.bind();


  }
  bind(){
      this.api.get("api/users").subscribe((result:any)=>{
      // console.log(result);
      this.users = result;

    })

  }

  update(id:any){
    // this.router.navigate(['admin/user/'+ id])
    console.log(id);



  }

  delete(id:any){
    this.api.delete("api/users/"+ id).subscribe((result:any)=>{
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
      text: "User has been deleted.",
      icon: "success"
    });
  }
});
      this.bind();

    })

  }

}
