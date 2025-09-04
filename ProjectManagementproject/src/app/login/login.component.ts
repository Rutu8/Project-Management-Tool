import { Component, OnInit } from '@angular/core';
import { CommonService } from '../shared/common.service';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  logindata:any;
  message:any;
  constructor(private api:CommonService, private router:Router){}

  ngOnInit(): void {
    this.logindata = new FormGroup({
      email: new FormControl("", Validators.compose([Validators.required, Validators.email])),
      password: new FormControl("", Validators.compose([Validators.required]))
    })
  }

  login(data:any){
     this.api.post("api/authentication/login", data).subscribe((result:any)=>{
      console.log(result);
      localStorage.setItem("usertype", result.usertype)
      localStorage.setItem("token", result.token)
      localStorage.setItem("expiration", result.expiration)
      localStorage.setItem("User Id", result.userId)

       if (result.status === "Failed") {
      this.message = result.message;
        Swal.fire({
  icon: "error",
  title: "Oops...",
  text: this.message,
});
    } else {
      if (result.usertype === "Admin") {
        Swal.fire({
  title: result.message,
  icon: "success",
  draggable: true
});
        this.router.navigate(["/admin"]);
      } else if (result.usertype === "User") {
        Swal.fire({
  title: result.message,
  icon: "success",
  draggable: true
});
        this.router.navigate(["/user"]);
      } else {
        // this.message = "Invalid Credential";
        Swal.fire({
  icon: "error",
  title: "Oops...",
  text: result.message,
});
        console.log(result.message);
      }
    }
  });
    }

  //    get f(){
  //   return this.logindata.controls;
  // }



  }


