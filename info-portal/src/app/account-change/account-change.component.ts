import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { UserViewModel } from '../models/account/userViewModel';
import { AccountService } from '../services/account.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-account-change',
  templateUrl: './account-change.component.html',
  styleUrls: ['./account-change.component.css']
})
export class AccountChangeComponent implements OnInit {
  user: UserViewModel;

  constructor(
    private accountService: AccountService,
    private location: Location,
    private route: ActivatedRoute
    ) { }

  getUser(): void {
    this.accountService.getUser().subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.getUser();
  }

  save(): void {
    this.accountService.editUser(this.user);
  }

  savePassword(oldPass: string, newPass: string, confirmPass: string): void {
    this.accountService.changePassword({ OldPassword: oldPass, NewPassword: newPass, ConfirmPassword: confirmPass});
  }

  goBack(): void {
    this.location.back();
  }
}
