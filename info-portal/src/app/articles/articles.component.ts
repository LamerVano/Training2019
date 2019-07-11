import { Component, OnInit } from '@angular/core';
import { Article } from '../models/article';
import { Location } from '@angular/common';

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
  id: number;

  constructor(
    private articleService: ArticleService,
    private route: ActivatedRoute,
    private location: Location
  ) { }

  ngOnInit() {
    if (this.route.snapshot.paramMap.has('id')) {
      this.id = +this.route.snapshot.paramMap.get('id');
      this.getArticles(this.id);
    } else {
      this.getAllArticles();
    }
  }

  getAllArticles(): void {
    this.articleService.getAllArticles()
      .subscribe(articles => this.articles = articles);
  }

  getArticles(id: number): void {
    this.articleService.getArticles(id)
      .subscribe(articles => this.articles = articles);
  }

  delete(article: Article): void {
    this.articles = this.articles.filter(c => c !== article);
    this.articleService.deleteArticle(article)
      .subscribe();
  }

  goBack(): void {
    this.location.back();
  }
}
