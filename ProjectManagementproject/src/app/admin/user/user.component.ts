import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonService } from 'src/app/shared/common.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
})
export class UserComponent implements OnInit {
  formdata: any;
  id: any;
  selectedFile: File | null = null;
  message: string = '';

  constructor(private api: CommonService,private route: ActivatedRoute,private router: Router) {
    this.id = this.route.snapshot.paramMap.get('id');
    console.log(this.id);
  }

  ngOnInit(): void {
    this.formdata = new FormGroup({
      id: new FormControl(''),
      name: new FormControl('', Validators.compose([Validators.required])),
      email: new FormControl('',Validators.compose([Validators.required, Validators.pattern('^\\s*[a-zA-Z0-9._%+-]+@gmail\\.com\\s*$')])),
      mobileNo: new FormControl('', Validators.compose([Validators.required,Validators.pattern('^[6-9][0-9]{9}$')])),
      password: new FormControl('',Validators.compose([Validators.required])),
      usertype: new FormControl('', Validators.compose([Validators.required])),
    });
    this.api.get('api/users/' + this.id).subscribe((result: any) => {
      this.formdata.patchValue({
        id: result.id,
        name: result.name,
        email: result.email,
        mobileNo: result.mobileNo,
        password: result.password,
        usertype: result.usertype,
      });
    });
  }


  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  onUpload() {
    if (!this.selectedFile) {
      this.message = 'Please select a file!';
      return;
    }
    this.api.uploadExcel('api/users/UploadExcelFile/file', this.selectedFile).subscribe({
        next: (res) => {
          this.message = 'File uploaded and data saved successfully!';
        },
        error: (err) => {
          this.message = 'Upload failed: ' + (err?.error || 'Unknown error');
        },
      });
  }

  submit(data: any) {
    console.log(data);
     if (this.formdata.invalid) {
    this.formdata.markAllAsTouched(); 
    Swal.fire({
      icon: 'error',
      title: 'Form Incomplete',
      text: 'Please fill in all required fields correctly.',
    });
    return;
  }

    if (this.id == null) {
      this.api.post('api/Users', data).subscribe((result: any) => {
        console.log(result);
        Swal.fire({
          position: 'center',
          icon: 'success',
          title: 'Your User has been saved',
          showConfirmButton: false,
          timer: 1500,
        }).then(() => {
    this.router.navigate(['/admin/users']);
  });
      // this.router.navigate(['/admin/users']);
      });
    } else {
      this.api.put('api/Users/' + this.id, data).subscribe((result: any) => {
        console.log(result);
        Swal.fire({
          position: 'center',
          icon: 'success',
          title: 'Your User has been Updated',
          showConfirmButton: false,
          timer: 1500,
        }).then(() => {
    this.router.navigate(['/admin/users']);
  });
      });

    }
  }


}
