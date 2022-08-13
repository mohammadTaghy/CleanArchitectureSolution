import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpParams
} from '@angular/common/http';
import { take, exhaustMap, map } from 'rxjs/operators';
import { Store } from '@ngrx/store';

import * as fromCmsApp from '../store/cms.reducer';

@Injectable({ providedIn: 'root' })
export class AuthInterceptorService implements HttpInterceptor {
  constructor(private store: Store<fromCmsApp.CmsState>) { }

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    return this.store.select('loginState').pipe(
      take(1),
      map(loginState => {
        return loginState.user;
      }),
      exhaustMap(user => {
        if (!user) {
          return next.handle(req);
        }
        const modifiedReq = req.clone({
          params: new HttpParams().set('token', user.Token)
        });
        return next.handle(modifiedReq);
      })
    );
  }
}
