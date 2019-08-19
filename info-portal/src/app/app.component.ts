import { Component, OnInit } from '@angular/core';
import { User } from './models/account/user';
import { Router } from '@angular/router';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'info-portal';
  currentUser: User;
  isAdmin: boolean;

  constructor(
    private router: Router,
    private accountService: AccountService
  ) { }

  public isAdminFunc(): void {
    this.accountService.isAdmin().subscribe(isadmin => this.isAdmin = isadmin);
  }

  ngOnInit(): void {
    this.isAdminFunc();
    this.accountService.currentUser.subscribe(user => this.currentUser = user);
  }

  onChange(): void {
    this.isAdminFunc();
  }

  logout() {
    this.accountService.logout();
    this.router.navigate(['/categories']);
  }
}
