import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonService } from 'src/app/shared/common.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css'],
})
export class TasksComponent implements OnInit {
  id: any;
  projectid: any;
  jobs: any;
  project: any;
  constructor(private api: CommonService, private router: Router) {}
  ngOnInit(): void {
    this.id = localStorage.getItem('User Id');
    this.api.get('api/users/jobs/' + this.id).subscribe((result: any) => {
      console.log(result);
      this.jobs = result;

      result.filter((t: any) => {
        this.projectid = t.job.projectid;
        this.api.get('api/projects/project/' + this.projectid);
        this.project = result;
      });
    });
  }
}
