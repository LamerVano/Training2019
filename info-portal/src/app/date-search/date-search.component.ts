import { Component, OnInit } from '@angular/core';
import { ArticleService } from '../services/article.service';
import { Subject, Observable } from 'rxjs';
import { Article } from '../models/article';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-date-search',
  templateUrl: './date-search.component.html',
  styleUrls: ['./date-search.component.css']
})
export class DateSearchComponent implements OnInit {
  articles$: Observable<Article[]>;
  private searchTerms = new Subject<string>();

  constructor(private articleService: ArticleService) { }

  search(term: string): void {
    this.searchTerms.next(term);
  }

  ngOnInit() {
    this.articles$ = this.searchTerms.pipe(
      // wait 300ms after each keystroke before considering the term
      debounceTime(300),

      // ignore new term if same as previous term
      distinctUntilChanged(),

      // switch to new search observable each time the term changes
      switchMap((term: string) => this.articleService.searchArticlesForDate(term)),
    );
  }
}
