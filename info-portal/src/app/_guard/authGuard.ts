import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { AccountService } from '../services/account.service';
import { User } from '../models/account/user';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  currentUser: User;

  constructor(
    private router: Router,
    private accountService: AccountService
  ) { }

  getUser(): void {
    this.accountService.currentUser.subscribe(user => this.currentUser = user);
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    this.getUser();
    if (this.currentUser) {
      // logged in so return true
      return true;
    }

    // not logged in so redirect to login page with the return url
    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    return false;
  }
}
