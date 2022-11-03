import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Store } from "@ngrx/store";
import { Observable, of } from "rxjs";
import { catchError, map } from "rxjs/operators";
import * as fromCmsApp from "../../cms/store/cms.reducer"
import * as LoginActions from "../../cms/auth/store/login.action";
@Injectable({
  providedIn: 'root'
})
export class CallAPIComponent {
  constructor(private http: HttpClient) { }
  PostApi<TRequest, TResponse>(url: string, requestBody: TRequest): Observable<TResponse> {
     let options_ = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Accept":"application/json"
      },
       
      )
    };
    console.log(requestBody);
    return this.http.post<TResponse>(url, requestBody, options_)
      //.pipe(
      //map(res => { return res; }),
      //catchError(err => {
      //  console.log(err);
      //  return of(err);
      //}))
      ;
  }
  GetApi<TResponse>(url: string, queryParametter: string): Observable<TResponse> {
    let options_ = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Accept": "application/json"
      },

      )
    };
    url += queryParametter != "" ? "&" + queryParametter : "";
    console.log("get api ?" + url);
    return this.http.get<TResponse>(url, options_)
      //.pipe(
      //map(res => { return res; }),
      //catchError(err => {
      //  console.log(err);
      //  return of(err);
      //}))
      ;
  }
}
