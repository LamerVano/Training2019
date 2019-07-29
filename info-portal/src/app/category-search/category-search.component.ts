import { Component, OnInit } from '@angular/core';
import { Category } from '../models/category';
import { Observable, Subject } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { switchMap, distinctUntilChanged, debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-category-search',
  templateUrl: './category-search.component.html',
  styleUrls: ['./category-search.component.css']
})
export class CategorySearchComponent implements OnInit {
  categories$: Observable<Category[]>;
  private searchTerms = new Subject<string>();

  constructor(private categoryService: CategoryService) { }

  search(term: string): void {
    this.searchTerms.next(term);
  }

  ngOnInit() {
    this.categories$ = this.searchTerms.pipe(
      // wait 300ms after each keystroke before considering the term
      debounceTime(300),

      // ignore new term if same as previous term
      distinctUntilChanged(),

      // switch to new search observable each time the term changes
      switchMap((term: string) => this.categoryService.searchCategories(term)),
    );
  }
}
