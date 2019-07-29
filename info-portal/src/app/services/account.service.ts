import { Injectable, OnDestroy } from '@angular/core';

import { RegisterBindingModel } from '../models/account/registerBindingModel';
import { LoginBindingModel } from '../models/account/loginBindingModel';

import { Observable, of, BehaviorSubject } from 'rxjs';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import { catchError, map, tap } from 'rxjs/operators';
import { User } from '../models/account/user';
import { UserViewModel } from '../models/account/userViewModel';
import { MessageService } from '../message.service';
import { SetPasswordBindingModel, ChangePasswordBindingModel } from '../models/account/зasswordBindingModel';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private accountUrl = 'http://localhost:50592/api/Account';

  private tokenUrl = `http://localhost:50592/Token`;

  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(
    private http: HttpClient,
    private messageService: MessageService
  ) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  register(registerBindingModel: RegisterBindingModel): Observable<any> {

    const url = `${this.accountUrl}/Register`;

    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };

    return this.http.post<RegisterBindingModel>(url, registerBindingModel, httpOptions)
      .pipe(catchError(this.handleError<any>('register')));
  }

  public get currentUserValue1(): User {
    this.log(`${this.currentUserSubject.value.access_token} ${this.currentUserSubject.value.userName}`);
    return this.currentUserSubject.value;
  }

  login(loginModel: LoginBindingModel) {
    const body = `grant_type=password&username=${loginModel.UserName}&password=${loginModel.Password}`;

    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' })
    };

    return this.http.post<any>(this.tokenUrl, body, httpOptions)
      .pipe(map(user => {
        if (user && user.access_token) {
          this.log(JSON.stringify(user));
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user);
        }

        return user;
      },
      catchError(this.handleError<any>('login'))));
  }

  logout() {
    const url = `${this.accountUrl}/Logout`;

    this.http.get<any>(url).pipe(catchError(this.handleError<any>('logout')));

    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  deleteAccount(userName: string): Observable<any> {
    const url = `${this.accountUrl}/Delete/${userName}`;
    return this.http.delete<any>(url)
      .pipe(catchError(this.handleError<any>('deleteAccount')));
  }

  getId(): Observable<string> {
    const url = `${this.accountUrl}/GetId`;

    return this.http.get<string>(url)
      .pipe(catchError(this.handleError<string>('getId')));
  }

  getName(id: string): Observable<string> {
    const url = `${this.accountUrl}/GetName/${id}`;

    return this.http.get<string>(url)
      .pipe(catchError(this.handleError<string>('getName')));
  }

  getAllUsers(): Observable<UserViewModel[]> {
    return this.http.get<UserViewModel[]>(`${this.accountUrl}/AllUsers`)
      .pipe(catchError(this.handleError<UserViewModel[]>('getAllUser', [])));
  }

  getUserById(id: string): Observable<UserViewModel> {
    return this.http.get<UserViewModel>(`${this.accountUrl}/${id}`)
      .pipe(catchError(this.handleError<UserViewModel>('getUser')));
  }

  getUser(): Observable<UserViewModel> {
    return this.http.get<UserViewModel>(this.accountUrl)
      .pipe(catchError(this.handleError<UserViewModel>('getUser')));
  }

  editUser(user: UserViewModel): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };

    return this.http.put<UserViewModel>(`${this.accountUrl}/${user.Id}`, user, httpOptions)
      .pipe(catchError(this.handleError<UserViewModel>('editUser')));
  }

  deleteUser(user: UserViewModel): Observable<any> {
    return this.http.delete(`${this.accountUrl}/${user.Id}`)
      .pipe(catchError(this.handleError<any>('editUser')));
  }

  setPassword(model: ChangePasswordBindingModel, id: string): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return this.http.post<ChangePasswordBindingModel>(`${this.accountUrl}/SetPassword/${id}`, model, httpOptions)
      .pipe(catchError(this.handleError<ChangePasswordBindingModel>('setPassword')));
  }

  changePassword(model: ChangePasswordBindingModel): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return this.http.post<ChangePasswordBindingModel>(`${this.accountUrl}/ChangePassword`, model, httpOptions)
      .pipe(catchError(this.handleError<ChangePasswordBindingModel>('changePassword')));
  }

  isAdmin(): Observable<boolean> {
    return this.http.get<boolean>(`${this.accountUrl}/isAdmin`)
      .pipe(catchError(this.handleError<boolean>('isAdmin')));
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
