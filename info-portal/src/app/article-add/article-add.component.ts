import { Component, OnInit } from '@angular/core';
import { Article } from '../models/article';
import { Location } from '@angular/common';
import { Observable } from 'rxjs';

import { ArticleService } from '../article.service';
import { ActivatedRoute } from '@angular/router';
import { CategoryReferences } from '../models/categoryReferences';
import { CategoryService } from '../category.service';

@Component({
  selector: 'app-article-add',
  templateUrl: './article-add.component.html',
  styleUrls: ['./article-add.component.css']
})
export class ArticleAddComponent implements OnInit {

  article: Article;
  image: File;

  constructor(
    private route: ActivatedRoute,
    private articleService: ArticleService,
    private location: Location,
    private categoryService: CategoryService
  ) { }

  ngOnInit() {
    this.getArticle();
    if (this.route.snapshot.paramMap.has('id')) {
      this.categoryService.getCategory(+this.route.snapshot.paramMap.get('id'));
    }
  }

  getArticle(): void {
    this.article = { CategoryRefs: { Refs: [] }, ArticleRefs: { Refs: [] } } as Article;
  }

  add(): void {
    // if ( !this.article.Name || !this.article.Language || !this.article.Video) { return; }
    this.articleService.addArticle(this.article)
      .subscribe(() => this.goBack());
  }

  goBack(): void {
    this.location.back();
  }

}
