import { Component, OnInit } from '@angular/core';
import { UserViewModel } from '../models/account/userViewModel';
import { AccountService } from '../services/account.service';
import { Location } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-admin-user-change',
  templateUrl: './admin-user-change.component.html',
  styleUrls: ['./admin-user-change.component.css']
})
export class AdminUserChangeComponent implements OnInit {
  user: UserViewModel;
  isAdmin: boolean;

  constructor(
    private accountService: AccountService,
    private location: Location,
    private route: ActivatedRoute
    ) { }

  isAdminFunc(): void {
    this.accountService.isAdmin().subscribe(res => this.isAdmin = res);
  }

  getUser(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.accountService.getUserById(id).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.isAdminFunc();
    this.getUser();
  }

  save(): void {
    this.accountService.editUser(this.user).subscribe();
  }

  savePassword(oldPass: string, newPass: string, confirmPass: string): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.accountService.setPassword({ OldPassword: oldPass, NewPassword: newPass, ConfirmPassword: confirmPass }, id);
  }

  goBack(): void {
    this.location.back();
  }
}
