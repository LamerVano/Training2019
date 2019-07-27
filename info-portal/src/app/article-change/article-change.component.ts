import { Component, OnInit } from '@angular/core';
import { Article } from '../models/article';

import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';

import { ArticleService } from '../services/article.service';
import { AccountService } from '../services/account.service';
import { User } from '../models/account/user';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-article-change',
  templateUrl: './article-change.component.html',
  styleUrls: ['./article-change.component.css']
})
export class ArticleChangeComponent implements OnInit {
  image: File;
  article: Article;
  currentUser: User;
  isAdmin: boolean;

  constructor(
    private route: ActivatedRoute,
    private articleService: ArticleService,
    private location: Location,
    private accountService: AccountService,
    private router: Router,
    private messageService: MessageService
  ) { }

  getCurrentUser(): void {
    this.accountService.currentUser.subscribe(user => {
      if (!user) {
        this.log('You not Login');
        this.router.navigate(['/articles']);
      }
      this.currentUser = user;
    });
  }

  isAdminFunc(): void {
    this.accountService.isAdmin().subscribe(admin => this.isAdmin = admin);
  }

  ngOnInit() {
    this.getCurrentUser();
    this.getArticle();
    if (this.currentUser.id !== this.article.UserId && !this.isAdmin) {
      this.log('It\'s not your article');
      this.router.navigate(['/articles']);
    }
  }

  getArticle(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.articleService.getArticle(id)
      .subscribe(article => {
        this.article = article;
        if (!this.article.ArticleRefs) {
          this.article.ArticleRefs.Refs = [];
        }
        if (!this.article.CategoryRefs) {
          this.article.CategoryRefs.Refs = [];
        }
      });
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
      .subscribe(() => {
        this.log('Save Succesed');
        this.goBack();
      });
  }

  savePicture(): void {
    this.articleService.updatePicture(this.image, this.article.Id)
      .subscribe(() => {
        this.log('Save Succesed');
        this.goBack();
      });
  }

  log(message: string): void {
    this.messageService.add(message);
  }
}
