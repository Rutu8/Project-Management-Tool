import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { CommonService } from 'src/app/shared/common.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css'],
})
export class ProjectComponent implements OnInit {
  formdata: any;
  id: any;
  data: any;

  constructor(
    private api: CommonService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.id = this.route.snapshot.paramMap.get('id');
    // this.ownerId = this.route.snapshot.paramMap.get("ownerId");
    // this.ownerId = this.route.snapshot.params['ownerId'];

    // console.log(this.ownerId);
  }

  ngOnInit(): void {
    this.bind();
  }

  bind() {
    this.formdata = new FormGroup({
      id: new FormControl(''),
      name: new FormControl(''),
      description: new FormControl(''),
      ownerId: new FormControl(''),
    });
    this.api.get('api/projects/project/' + this.id).subscribe((result: any) => {
      console.log(result);

      this.formdata.patchValue({
        id: result.id,
        name: result.name,
        description: result.description,
        ownerId: result.ownerId,
      });

      this.api.get('api/projects/owner/' + result.ownerId).subscribe((data: any) => {
          console.log(data);
          this.data = data;
          // console.log(this.data.name);
        });
    });
  }

  submit(data: any) {
    console.log(data);
    this.api.put('api/projects/' + this.id, data).subscribe((result: any) => {
      Swal.fire({
        position: 'center',
        icon: 'success',
        title: 'Project Updated',
        showConfirmButton: false,
        timer: 1500,
      });
      console.log(result);
      this.router.navigate(['/admin/projects']);
    });
    this.bind();

    // this.api.post("api/projects/", data).subscribe((result:any)=>{
    //   console.log(result);

    // })
  }
}
