import { HttpClient, HttpEvent, HttpEventType, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Config } from "../config/config"

import { json } from "stream/consumers";
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
  addVersion(url: string): string {

    if (url.search("api-version") == -1) {
      console.log(url);
      let sign: string = url.search("\\?") == -1 ? "?" : "&";
      console.log(url + sign);
      url += sign + "api-version=" + Config.version;
      console.log(url);
    }
    return url;
  }
  postApi<TRequest, TResponse>(url: string, requestBody: TRequest): Observable<TResponse> {
    //console.log(url);
    //console.log(this.options);
   //console.log(requestBody);

    return this.http.post<TResponse>(this.addVersion(url), JSON.stringify(requestBody), this.options);
    //return of();
  }
  putApi<TRequest, TResponse>(url: string, requestBody: TRequest): Observable<TResponse> {
   //console.log(url);
   //console.log(this.options);
   //console.log(requestBody);
    
    return this.http.put<TResponse>(this.addVersion(url), requestBody, this.options);
  }
  deleteApi<TRequest, TResponse>(url: string): Observable<TResponse> {

    return this.http.delete<TResponse>(this.addVersion(url), this.options);
  }
  GetApi<TResponse>(url: string): Observable<TResponse> {
    
    //console.log("get api ?" + url);
    return this.http.get<TResponse>(this.addVersion(url.trim()), this.options);
  }
  PostFile(formData: FormData, url: string) {
   //console.log("PostFile");
   //console.log(file.size);
   //console.log(fileSize);
    const headers = new HttpHeaders();
    headers.set("Content-Type", "multipart/form-data; boundary=--------------------------930544582080383026463173");
    headers.set("Accept", "*/*");
    headers.set("Accept-Encoding", "gzip, deflate, br");
    headers.set("Connection", "keep-alive"); 
    headers.set("Cache-Control", "no-cache"); 
    headers.set("Access-Control-Allow-Origin", "*"); 
   //console.log("PostFile")
    return this.http
      .post(url, formData, {
        headers: headers,
        reportProgress: true,
        observe: 'events'
      })
      .pipe(
        map((event) => {
         //console.log("event");
         //console.log(event);
          switch (event.type) {
            case HttpEventType.UploadProgress:
              const progress = Math.round((100 * event.loaded) / event.total);
              return { status: 'progress', message: progress };

            case HttpEventType.Response:
              return event.body;
            default:
              return `Unhandled event: ${event.type}`;
          }
        })
      );
  }
}
