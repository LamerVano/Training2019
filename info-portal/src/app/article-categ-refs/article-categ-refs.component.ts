import { Component, OnInit, Input } from '@angular/core';
import { CategoryReferences } from '../models/categoryReferences';
import { Category } from '../models/category';
import { CategoryService } from '../services/category.service';
import { Router } from '@angular/router';
import { User } from '../models/account/user';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-article-categ-refs',
  templateUrl: './article-categ-refs.component.html',
  styleUrls: ['./article-categ-refs.component.css']
})
export class ArticleCategRefsComponent implements OnInit {

  @Input() categories: CategoryReferences;
  allCategory: Category[];
  selectedCategory: Category;
  currentUser: User;

  constructor(
    private categoryService: CategoryService,
    private router: Router,
    private accountService: AccountService
  ) { }

  getCurrentUser(): void {
    this.accountService.currentUser.subscribe(user => this.currentUser = user);
  }

  ngOnInit() {
    this.getCurrentUser();
    this.getArticles();
  }

  getArticles(): void {
    this.categoryService.getCategories()
      .subscribe(categories => this.allCategory = categories);
  }

  delete(category: Category): void {
    this.categories.Refs.splice(this.categories.Refs.findIndex(c => c === category), 1);
  }

  add(category: Category): void {
    if (this.categories.Refs == null) {
      this.categories.Refs = [category];
    } else {
      this.categories.Refs.push(category);
    }
  }

}
