import { Component, OnInit } from '@angular/core';
import { ArticleReferences } from '../models/articleReferences';
import { Article } from '../models/article';

import { ArticleService } from '../article.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-article-add',
  templateUrl: './article-add.component.html',
  styleUrls: ['./article-add.component.css']
})
export class ArticleAddComponent implements OnInit {

  article: Article;

  constructor(
    private route: ActivatedRoute,
    private articleService: ArticleService
    ) { }

  ngOnInit() {
    this.getArticle();
  }

  getArticle(): void {
    this.article = {} as Article;
  }

  add(): void {
    this.article.Name = this.article.Name.trim();
    if ( !this.article.Name || !this.article.Language || !this.article.Video) { return; }
    this.articleService.addArticle(this.article)
      .subscribe();
  }
}
