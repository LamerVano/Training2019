import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

import { AccountService } from '../services/account.service';
import { User } from '../models/account/user';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  currentUser: User;

  constructor(private accountService: AccountService) { }

  getUser(): void {
    this.accountService.currentUser.subscribe(user => this.currentUser = user);
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // add authorization header with jwt token if available
    this.getUser();
    if (this.currentUser && this.currentUser.access_token) {
      console.log(`${this.currentUser.userName} token add to Header`);
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.currentUser.access_token}`
        }

      });
    }

    return next.handle(request);
  }
}
