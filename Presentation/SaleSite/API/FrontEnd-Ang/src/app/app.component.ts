import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    console.log("OnInit");
    //let options_: any = {
    //  headers: new HttpHeaders({
    //    "Content-Type": "application/json",
    //    "Accept":"application/json"
    //  })
    //};
    //let data = JSON.stringify({
    //  UserName: "Taghy@gmail.com",
    //  password: "123456",
    //  MobileNumber: "09384563280",
    //  Email: "Taghy@gmail.com"
    //});
    //console.log(data);
    //this.http.post(
    //  "http://localhost:18023/api/User/CreateUser", data, options_)
    //  .pipe((response_: any) => {
    //  console.log(response_);
    //  return response_;
    //}).subscribe(response => console.log(response));
  }
  title = 'FrontEnd-Ang';

}
