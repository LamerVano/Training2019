import { Component, OnInit } from '@angular/core';
import { LoginBindingModel } from '../models/account/loginBindingModel';
import { AccountService } from '../services/account.service';
import { Location } from '@angular/common';
import { User } from '../models/account/user';
import { Router } from '@angular/router';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {

  currentUser: User;

  constructor(
    private accountService: AccountService,
    protected location: Location,
    private router: Router,
    private messageService: MessageService
  ) { }

  ngOnInit(): void {
    this.accountService.currentUser.subscribe(user => {
      if (user) {
        this.log('You already LogIn');
        this.router.navigate(['/articlies']);
      }
      this.currentUser = user;
    });
  }

  login(userName: string, password: string): void {
    const model = { UserName: userName, Password: password } as LoginBindingModel;
    this.accountService.login(model)
      .subscribe(() => {
        this.log('LogIn Succesed');
        this.router.navigate(['/articles']);
      } );
  }

  goBack(): void {
    this.location.back();
  }

  log(message: string): void {
    this.messageService.add(message);
  }
}
