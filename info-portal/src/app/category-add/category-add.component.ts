import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category';
import { Location } from '@angular/common';
import { AccountService } from '../services/account.service';
import { User } from '../models/account/user';
import { Router } from '@angular/router';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-category-add',
  templateUrl: './category-add.component.html',
  styleUrls: ['./category-add.component.css']
})
export class CategoryAddComponent implements OnInit {

  currentUser: User;

  constructor(
    private categoryService: CategoryService,
    private location: Location,
    private accountService: AccountService,
    private router: Router,
    private messageService: MessageService
  ) { }

  getCurrentUser(): void {
    this.accountService.currentUser.subscribe(user => {
      if (!user) {
        this.log('You not Login');
        this.router.navigate(['/categories']);
      }
      this.currentUser = user;
    });
  }

  ngOnInit() {
    this.getCurrentUser();
  }

  add(name: string): void {
    name = name.trim();
    if (!name) { return; }
    this.categoryService.addCategory({ Name: name } as Category)
      .subscribe(() => {
        this.log('Save Succesed');
        this.goBack();
      });
  }

  goBack(): void {
    this.location.back();
  }

  log(message: string): void {
    this.messageService.add(message);
  }
}
