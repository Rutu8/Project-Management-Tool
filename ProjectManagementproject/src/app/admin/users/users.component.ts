import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonService } from 'src/app/shared/common.service';

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
      this.bind();

    })

  }

}
