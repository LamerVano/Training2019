import { Component, OnInit } from '@angular/core';
import { Article } from '../models/article';

import { ArticleService } from '../article.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.css']
})
export class ArticlesComponent implements OnInit {

  articles: Article[];
  selectedArticle: Article;

  constructor(
    private articleService: ArticleService,
    private route: ActivatedRoute
    ) { }

  ngOnInit() {
    this.getArticles();
  }

  getArticles(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.articleService.getArticles(id)
      .subscribe(articles => this.articles = articles);
  }

  delete(article: Article): void {
    this.articles = this.articles.filter(c => c !== article);
    this.articleService.deleteArticle(article)
      .subscribe();
  }
}
