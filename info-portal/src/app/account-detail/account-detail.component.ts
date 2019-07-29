import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { UserViewModel } from '../models/account/userViewModel';
import { Location } from '@angular/common';

@Component({
  selector: 'app-account-detail',
  templateUrl: './account-detail.component.html',
  styleUrls: ['./account-detail.component.css']
})
export class AccountDetailComponent implements OnInit {

  user: UserViewModel;

  constructor(
    private accountService: AccountService,
    private location: Location
  ) { }

  getUser(): void {
    this.accountService.getUser().subscribe(user => this.user = user);
  }

  ngOnInit() {
    this.getUser();
  }

  goBack(): void {
    this.location.back();
  }
}
