import { Component, OnInit, Input } from '@angular/core';
import { CategoryReferences } from '../models/categoryReferences';
import { Category } from '../models/category';
import { CategoryService } from '../category.service';

@Component({
  selector: 'app-article-categ-refs',
  templateUrl: './article-categ-refs.component.html',
  styleUrls: ['./article-categ-refs.component.css']
})
export class ArticleCategRefsComponent implements OnInit {

  @Input() categories: CategoryReferences;
  allCategory: Category[];
  selectedCategory: Category;

  constructor(private categoryService: CategoryService) { }

  ngOnInit() {
    this.getArticles();
  }

  getArticles(): void {
    this.categoryService.getCategories()
      .subscribe(categories => this.allCategory = categories);
  }

  delete(category: Category): void {
    this.categories.Refs.splice(this.categories.Refs.findIndex(c => c === category), 1);
  }

  add(category: Category): void {
    if (this.categories.Refs == null) {
      this.categories.Refs = [category];
    } else {
      this.categories.Refs.push(category);
    }
  }

}
