import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  baseUrl= "https://localhost:7171/";

  constructor(private http:HttpClient) { }

  get(path:string){
    return this.http.get(this.baseUrl+path)
  }

  post(path:string, body:any){
    return this.http.post(this.baseUrl+path, body)

  }

  put(path:string, body:any){
    return this.http.put(this.baseUrl + path, body)
  }

  delete(path:string){
    return this.http.delete(this.baseUrl+path)
  }

  getbyid(path:string){
    return this.http.get(this.baseUrl+path)
  }
}
