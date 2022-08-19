import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CallAPIComponent {
  constructor(private http: HttpClient) { }
  PostApi<TRequest, TResponse>(url: string, requestBody: TRequest): Observable<TResponse> {
    console.log('Post Api');
     let options_ = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Accept":"application/json"
      })
    };
    return this.http.post<TResponse>(url, requestBody, options_)
  }
}
