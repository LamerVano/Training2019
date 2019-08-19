import { Injectable } from '@angular/core';

import { Article } from '../models/article';
import { ArticleReference } from '../models/articleReference';

import { Observable, of } from 'rxjs';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  private articlesUrl = 'http://localhost:50592/api/Article';
  private pictureUrl = 'http://localhost:50592/Content/Articles';

  constructor(private http: HttpClient) { }

  getAllArticles(): Observable<Article[]> {
    const url = `${this.articlesUrl}/all`;
    return this.http.get<Article[]>(url)
      .pipe(catchError(this.handleError<Article[]>('getAllArticles', [])));
  }

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

  getPicture(pictureRef: string): Observable<File> {
    const url = `${this.pictureUrl}/${pictureRef}`;

    return this.http.get<File>(url)
      .pipe(catchError(this.handleError<File>('getPicture')));
  }

  updateArticle(article: Article): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return this.http.put(this.articlesUrl, article, httpOptions)
      .pipe(catchError(this.handleError<any>('updateArticle')));
  }

  updatePicture(image: File, id: number): Observable<any> {
    const data = new FormData();
    data.append('image', image);

    const url = `${this.articlesUrl}/image/${id}`;

    return this.http.put(url, data)
      .pipe(catchError(this.handleError<any>('updatePicture')));
  }

  addArticle(article: Article): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };

    return this.http.post<Article>(this.articlesUrl, article, httpOptions)
      .pipe(catchError(this.handleError<any>('addArticle')));
  }

  addPicture(image: File): Observable<any> {
    const data = new FormData();
    data.append('image', image);

    const url = `${this.articlesUrl}/image`;

    return this.http.post<any>(url, data)
      .pipe(catchError(this.handleError<any>('addPicture')));
  }

  deleteArticle(article: Article): Observable<Article> {
    const id = article.Id;
    const url = `${this.articlesUrl}/${id}`;
    return this.http.delete<Article>(url)
      .pipe(catchError(this.handleError<Article>('deleteArticle')));
  }

  searchArticles(term: string): Observable<Article[]> {
    if (!term.trim()) {
      // if not search term, return empty hero array.
      return of([]);
    }
    return this.http.get<Article[]>(`${this.articlesUrl}/?name=${term}`).pipe(
      tap(_ => this.log(`found articles matching "${term}"`)),
      catchError(this.handleError<Article[]>('searchArticles', []))
    );
  }

  searchArticlesForDate(date: string): Observable<Article[]> {
    if (!date) {
      // if not search term, return empty hero array.
      return of([]);
    }
    return this.http.get<Article[]>(`${this.articlesUrl}/?date=${date}`).pipe(
      tap(_ => this.log(`found articles matching for date "${date}"`)),
      catchError(this.handleError<Article[]>('searchArticlesForDate', []))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);

      this.log(`${operation} failed: ${error.message}`);

      return of(result as T);
    };
  }

  private log(message: string) {
    console.log(`AccountService: ${message}`);
  }
}
