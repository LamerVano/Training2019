import { Component, OnInit } from '@angular/core';
import { Article } from '../models/article';
import { Location } from '@angular/common';
import { Observable } from 'rxjs';

import { ArticleService } from '../services/article.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryReferences } from '../models/categoryReferences';
import { CategoryService } from '../services/category.service';
import { AccountService } from '../services/account.service';
import { User } from '../models/account/user';

@Component({
  selector: 'app-article-add',
  templateUrl: './article-add.component.html',
  styleUrls: ['./article-add.component.css']
})
export class ArticleAddComponent implements OnInit {

  article: Article;
  image: File;
  currentUser: User;

  constructor(
    private route: ActivatedRoute,
    private accountService: AccountService,
    private articleService: ArticleService,
    private location: Location,
    private categoryService: CategoryService,
    private router: Router
  ) { }

  getCurrentUser(): void {
    this.accountService.currentUser.subscribe(user => {
      if (!user) {
        this.router.navigate(['/articles']);
      }
      this.currentUser = user;
    });
  }

  ngOnInit() {
    this.getCurrentUser();
    this.getArticle();
    this.getId();
  }

  getArticle(): void {
    this.article = { CategoryRefs: { Refs: [] }, ArticleRefs: { Refs: [] } } as Article;
  }

  onChange(files: FileList) {
    this.image = files[0];
  }

  add(name: string, language: string, video: string): void {
    this.article.Name = name;
    this.article.Language = language;
    this.article.Video = video;

    if (this.image && this.image !== null) {
      this.articleService.addArticle(this.article)
        .subscribe(() => this.addPicture());
      return;
    }

    this.articleService.addArticle(this.article)
        .subscribe(() => this.goBack());
  }

  addPicture(): void {
    this.articleService.addPicture(this.image)
      .subscribe(() => this.goBack());
  }

  getId(): void {
    this.accountService.getId().subscribe(id => {
      console.log(id);
      this.article.UserId = id;
    });
  }

  goBack(): void {
    this.location.back();
  }

}
