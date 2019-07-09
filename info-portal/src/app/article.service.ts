import { Injectable } from '@angular/core';

import { Article } from './models/article';
import { ArticleReference } from './models/articleReference';

import { Observable, of } from 'rxjs';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  private articlesUrl = 'api/Article';

  constructor(private http: HttpClient) { }

  getArticles(id: number): Observable<Article[]> {
    const url = `${this.articlesUrl}/byCategory/${id}`;
    return this.http.get<Article[]>(url)
      .pipe(catchError(this.handleError<Article[]>('getArticles', [])));
  }

  getShortArticles(): Observable<ArticleReference[]> {
    return this.http.get<ArticleReference[]>(this.articlesUrl)
      .pipe(catchError(this.handleError<ArticleReference[]>('getShortArticles', [])));
  }

  getArticle(id: number): Observable<Article> {
    const url = `${this.articlesUrl}/${id}`;
    return this.http.get<Article>(url)
      .pipe(catchError(this.handleError<Article>('getArticle id=${id}')));
  }

  updateArticle(article: Article): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return this.http.put(this.articlesUrl, article, httpOptions)
      .pipe(catchError(this.handleError<any>('updateArticle')));
  }

  addArticle(article: Article): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return this.http.post<Article>(this.articlesUrl, article, httpOptions)
      .pipe(catchError(this.handleError<any>('updateArticle')));
  }

  deleteArticle(article: Article | number): Observable<Article> {
    const id = typeof article === 'number' ? article : article.Id;
    const url = `${this.articlesUrl}/${id}`;
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return this.http.delete<Article>(url, httpOptions)
    .pipe(catchError(this.handleError<Article>('deleteHero')));
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);

      return of(result as T);
    };
  }
}
