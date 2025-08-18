import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonService } from 'src/app/shared/common.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  formdata:any;
  id:any;

  constructor(private api:CommonService, private route:ActivatedRoute, private router:Router) {
    this.id = this.route.snapshot.paramMap.get("id");
    console.log(this.id);


  }

  ngOnInit(): void {
     this.formdata = new FormGroup({
      id:new FormControl(""),
      name: new FormControl("", Validators.compose([Validators.required])),
      email: new FormControl("",Validators.compose([Validators.required, Validators.email])),
      mobileNo: new FormControl("", Validators.compose([Validators.required])),
      password: new FormControl("", Validators.compose([Validators.required, Validators.maxLength(6)])),
      usertype: new FormControl("", Validators.compose([Validators.required]))
    })
    this.api.get("api/users/"+this.id).subscribe((result:any)=>{
      this.formdata.patchValue({
        id:result.id,
        name:result.name,
        email:result.email,
        mobileNo:result.mobileNo,
        password:result.password,
        usertype:result.usertype
      })
    })




  }




  submit(data:any){
    if(this.id == null){
       this.api.post("api/users", data).subscribe((result:any)=>{
          console.log(result);
        })
        this.router.navigate(["/admin/users"]);


      }
       else{
          this.api.put("api/users/" + this.id,data).subscribe((result:any)=>{
        console.log(result);
      })
      this.router.navigate(["/admin/users"]);

    }


  }

}
