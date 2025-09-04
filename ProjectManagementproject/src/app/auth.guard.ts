import { CanActivateFn, Router } from '@angular/router';
import { CommonService } from './shared/common.service';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const usertype = localStorage.getItem("usertype")
  const expectedtype = route.data?.['expectedtype'];

  // console.log("usertype"+usertype);

  // console.log("expected type"+expectedtype);

  if(usertype == expectedtype){
  return true;
  }

  return router.navigate(["/"])
};
