import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  baseUrl= "https://localhost:7171/";

  constructor(private http:HttpClient) { }

  get(path: string){
    return this.http.get(this.baseUrl+path)
  }

  // getexcel(path:string, {observe:'response', responseType:'blob'}){
  //   return this.http.get(this.baseUrl +  path)
  // }

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

  common(result:any){
     let filename = result.headers.get('content-disposition')?.split(';')[1].split('=')[1];
    let blob:Blob = result.body as Blob;
    let a = document.createElement('a');
    a.download = filename;
    a.href = window.URL.createObjectURL(blob);
    a.click();
  }
}
