import { Component, OnInit } from '@angular/core';
import { CommonService } from '../shared/common.service';
import { Router } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';

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
      email: new FormControl(""),
      password: new FormControl("")
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
    } else {
      if (result.usertype === "Admin") {
        this.router.navigate(["/admin"]);
      } else if (result.usertype === "User") {
        this.router.navigate(["/user"]);
      } else {
        this.message = "Invalid Credential";
        console.log(this.message);
      }
    }
  });
    }




  }


