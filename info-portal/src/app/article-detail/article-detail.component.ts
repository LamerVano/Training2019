import { Component, OnInit } from '@angular/core';
import { Article } from '../models/article';

import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { ArticleService } from '../services/article.service';

@Component({
  selector: 'app-article-detail',
  templateUrl: './article-detail.component.html',
  styleUrls: ['./article-detail.component.css']
})
export class ArticleDetailComponent implements OnInit {

  article: Article;

  constructor(
    private route: ActivatedRoute,
    private articleService: ArticleService,
    private location: Location
    ) { }

  ngOnInit() {
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

  save(): void {
    this.articleService.updateArticle(this.article)
      .subscribe(() => this.goBack());
  }

}
