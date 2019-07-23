import { Component, OnInit } from '@angular/core';
import { Article } from '../models/article';

import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';

import { ArticleService } from '../services/article.service';
import { AccountService } from '../services/account.service';
import { User } from '../models/account/user';

@Component({
  selector: 'app-article-change',
  templateUrl: './article-change.component.html',
  styleUrls: ['./article-change.component.css']
})
export class ArticleChangeComponent implements OnInit {
  image: File;
  article: Article;
  currentUser: User;

  constructor(
    private route: ActivatedRoute,
    private articleService: ArticleService,
    private location: Location,
    private accountService: AccountService,
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
    if (this.article.ArticleRefs.Refs == null) {
      this.article.ArticleRefs.Refs = [];
    }
    if (this.article.CategoryRefs.Refs == null) {
      this.article.CategoryRefs.Refs = [];
    }
  }

  getArticle(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.articleService.getArticle(id)
      .subscribe(article => this.article = article);
  }

  goBack(): void {
    this.location.back();
  }

  fileProgress(fileInput: any) {
    this.image = fileInput.target.files[0] as File;
  }

  onChange(files: FileList) {
    this.image = files[0];
  }

  save(): void {
    if (this.image && this.image !== null) {
      this.articleService.updateArticle(this.article)
        .subscribe(() => this.savePicture());
      return;
    }

    this.articleService.updateArticle(this.article)
      .subscribe(() => this.goBack());
  }

  savePicture(): void {
    this.articleService.updatePicture(this.image, this.article.Id)
      .subscribe(() => this.goBack());
  }

}
