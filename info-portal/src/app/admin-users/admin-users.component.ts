import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { UserViewModel } from '../models/account/userViewModel';
import { Location } from '@angular/common';

@Component({
  selector: 'app-admin-users',
  templateUrl: './admin-users.component.html',
  styleUrls: ['./admin-users.component.css']
})
export class AdminUsersComponent implements OnInit {

  users: UserViewModel[];
  isAdmin: boolean;

  constructor(
    private accountService: AccountService,
    private location: Location,
    ) { }

  isAdminFunc(): void {
    this.accountService.isAdmin().subscribe(res => this.isAdmin = res);
  }

  getUsers(): void {
    this.accountService.getAllUsers().subscribe(users => this.users = users);
  }

  ngOnInit(): void {
    this.isAdminFunc();
    this.getUsers();
  }

  delete(user: UserViewModel): void {
    this.accountService.deleteUser(user);
  }

  goBack(): void {
    this.location.back();
  }

}
