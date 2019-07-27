import { Component, OnInit } from '@angular/core';
import { Article } from '../models/article';

import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { ArticleService } from '../services/article.service';

import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-article-detail',
  templateUrl: './article-detail.component.html',
  styleUrls: ['./article-detail.component.css']
})
export class ArticleDetailComponent implements OnInit {

  article: Article;
  myurl: string;

  constructor(
    public sanitizer: DomSanitizer,
    private route: ActivatedRoute,
    private articleService: ArticleService,
    private location: Location
  ) { }

  ngOnInit() {
    this.getArticle();
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
        this.myurl = 'https://www.youtube.com/embed/' + article.Video;
      });
  }

  goBack(): void {
    this.location.back();
  }

  save(): void {
    this.articleService.updateArticle(this.article)
      .subscribe(() => this.goBack());
  }
}
