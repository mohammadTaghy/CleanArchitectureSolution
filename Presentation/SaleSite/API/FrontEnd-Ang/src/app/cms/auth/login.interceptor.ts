import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpParams,
  HttpResponse,
  HttpEvent,
  HttpErrorResponse
} from '@angular/common/http';
import { take, exhaustMap, map, tap, retryWhen, catchError } from 'rxjs/operators';
import { Store } from '@ngrx/store';

import * as fromCmsApp from '../store/cms.reducer';
import { ConstantNameString } from '../common/constant/constant.common';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';

@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
  constructor(private store: Store<fromCmsApp.CmsState<any>>, private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    //console.log("intercept1");
    //console.log(event instanceof HttpResponse);
    //console.log("responce Interceptor ");
    let modifiedReq = req.clone();
    if (localStorage.getItem(ConstantNameString.Token) != null) {
      //console.log(localStorage.getItem(ConstantNameString.Token));
      //console.log("set request header")
      const headers = req.headers.set(ConstantNameString.Token, localStorage.getItem(ConstantNameString.Token));
      modifiedReq = req.clone({ headers });
    }
    return this.store.select('loginState').pipe(
      take(1),
      map(loginState => {
        return loginState.user;
      }),
      exhaustMap(user => {
        if (!user) {
          return next.handle(modifiedReq).pipe(
            tap({
              next: (event) => {
                //console.log("intercept");
                //console.log(event);
                if ((event instanceof HttpResponse) && event.headers.has(ConstantNameString.Token))
                  localStorage.setItem(ConstantNameString.Token, event.headers.get(ConstantNameString.Token));

              }
            }),
            catchError((error: HttpErrorResponse) => {
              //console.log("request error");
              //console.log(error);
              if (error?.status === 401) {
                location.href = "http://localhost:18023/admin/auth";
                //this.router.navigate(['/auth']);
              }
              return throwError(error.error.message);
            })
          )
        }
        return next.handle(modifiedReq);
      })
    );
    
  }

}

