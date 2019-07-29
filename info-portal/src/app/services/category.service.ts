import { Injectable } from '@angular/core';

import { Category } from '../models/category';

import { Observable, of } from 'rxjs';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class CategoryService {


  private categoriesUrl = 'http://localhost:50592/api/Category';

  constructor(private http: HttpClient) { }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.categoriesUrl)
      .pipe(catchError(this.handleError<Category[]>('getCategories', [])));
  }

  getCategory(id: number): Observable<Category> {
    const url = `${this.categoriesUrl}/${id}`;
    return this.http.get<Category>(url).pipe(
      catchError(this.handleError<Category>(`getCategory id=${id}`))
  );
  }

  updateCategory(category: Category): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return this.http.put(this.categoriesUrl, category, httpOptions).pipe(
      catchError(this.handleError<any>('updateCategory'))
    );
  }

  addCategory(category: Category): Observable<Category> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return this.http.post<Category>(this.categoriesUrl, category, httpOptions).pipe(
      catchError(this.handleError<Category>('addCategory'))
    );
  }

  deleteCategory(category: Category | number): Observable<Category> {
    const id = typeof category === 'number' ? category : category.Id;
    const url = `${this.categoriesUrl}/${id}`;
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return this.http.delete<Category>(url, httpOptions).pipe(
      catchError(this.handleError<Category>('deleteHero'))
    );
  }

  searchCategories(term: string): Observable<Category[]> {
    if (!term.trim()) {
      // if not search term, return empty hero array.
      return of([]);
    }
    return this.http.get<Category[]>(`${this.categoriesUrl}/?name=${term}`).pipe(
      tap(_ => this.log(`found categories matching "${term}"`)),
      catchError(this.handleError<Category[]>('searchCategories', []))
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
