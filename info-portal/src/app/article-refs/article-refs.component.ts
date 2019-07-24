import { Component, OnInit, Input } from '@angular/core';
import { ArticleReferences } from '../models/articleReferences';
import { ArticleReference } from '../models/articleReference';

import { ArticleService } from '../services/article.service';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';
import { User } from '../models/account/user';

@Component({
  selector: 'app-article-refs',
  templateUrl: './article-refs.component.html',
  styleUrls: ['./article-refs.component.css']
})
export class ArticleRefsComponent implements OnInit {

  @Input() article: ArticleReferences;
  allArticles: ArticleReference[];
  selectedArticle: ArticleReference;
  currentUser: User;

  constructor(
    private articleService: ArticleService,
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
    this.articleService.getShortArticles()
      .subscribe(articles => this.allArticles = articles);
  }

  delete(article: ArticleReference): void {
    this.article.Refs.splice(this.article.Refs.findIndex(c => c === article), 1);
  }

  add(article: ArticleReference): void {
    this.article.Refs.push(article);
  }
}
