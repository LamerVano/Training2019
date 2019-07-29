import { Component, OnInit } from '@angular/core';
import { Category } from '../models/category';

import { CategoryService } from '../services/category.service';
import { AppComponent } from '../app.component';
import { AccountService } from '../services/account.service';
import { User } from '../models/account/user';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {

  categories: Category[];
  selectedCategory: Category;
  currentUser: User;

  constructor(
    private categoryService: CategoryService,
    private accountService: AccountService,
    private messageService: MessageService
    ) { }

  ngOnInit() {
    this.getCategories();
    this.accountService.currentUser.subscribe(user => this.currentUser = user);
  }

  getCategories(): void {
    this.categoryService.getCategories()
      .subscribe(categories => this.categories = categories);
  }

  log(message: string): void {
    this.messageService.add(message);
  }

}
