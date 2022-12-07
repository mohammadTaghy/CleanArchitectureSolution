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
  options = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    },

    )
  };
  postApi<TRequest, TResponse>(url: string, requestBody: TRequest): Observable<TResponse> {
    //console.log(url);
    //console.log(this.options);
    //console.log(requestBody);
    return this.http.post<TResponse>(url, JSON.stringify(requestBody), this.options);
    //return of();
  }
  putApi<TRequest, TResponse>(url: string, requestBody: TRequest): Observable<TResponse> {
    console.log(url);
    console.log(this.options);
    console.log(requestBody);
    
    return this.http.put<TResponse>(url, requestBody, this.options);
  }
  deleteApi<TRequest, TResponse>(url: string): Observable<TResponse> {

    return this.http.delete<TResponse>(url, this.options);
  }
  GetApi<TResponse>(url: string): Observable<TResponse> {
    
    console.log("get api ?" + url);
    return this.http.get<TResponse>(url, this.options);
  }
}
