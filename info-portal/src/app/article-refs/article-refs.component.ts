import { Component, OnInit, Input } from '@angular/core';
import { ArticleReferences } from '../models/articleReferences';
import { ArticleReference } from '../models/articleReference';

import { ArticleService } from '../article.service';

@Component({
  selector: 'app-article-refs',
  templateUrl: './article-refs.component.html',
  styleUrls: ['./article-refs.component.css']
})
export class ArticleRefsComponent implements OnInit {

  @Input() article: ArticleReferences;
  allArticles: ArticleReference[];
  selectedArticle: ArticleReference;

  constructor(private articleService: ArticleService) { }

  ngOnInit() {
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
