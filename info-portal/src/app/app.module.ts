import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS  } from '@angular/common/http';

import { AppComponent } from './app.component';
import { ArticlesComponent } from './articles/articles.component';
import { CategoriesComponent } from './categories/categories.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';
import { AppRoutingModule } from './app-routing.module';
import { ArticleDetailComponent } from './article-detail/article-detail.component';
import { ArticleAddComponent } from './article-add/article-add.component';
import { ArticleRefsComponent } from './article-refs/article-refs.component';
import { ArticleCategRefsComponent } from './article-categ-refs/article-categ-refs.component';
import { RegisterComponent } from './register/register.component';
import { LogInComponent } from './log-in/log-in.component';
import { CategoryAddComponent } from './category-add/category-add.component';
import { ArticleChangeComponent } from './article-change/article-change.component';
import { ArticleSearchComponent } from './article-search/article-search.component';
import { CategorySearchComponent } from './category-search/category-search.component';
import { DateSearchComponent } from './date-search/date-search.component';
import { AdminUsersComponent } from './admin-users/admin-users.component';
import { AccountDetailComponent } from './account-detail/account-detail.component';
import { AccountChangeComponent } from './account-change/account-change.component';
import { AdminUserChangeComponent } from './admin-user-change/admin-user-change.component';
import { JwtInterceptor } from './_helpers/jwtInterceptor';
import { MessagesComponent } from './messages/messages.component';

@NgModule({
  declarations: [
    AppComponent,
    ArticlesComponent,
    CategoriesComponent,
    CategoryDetailComponent,
    ArticleDetailComponent,
    ArticleAddComponent,
    ArticleRefsComponent,
    ArticleCategRefsComponent,
    RegisterComponent,
    LogInComponent,
    CategoryAddComponent,
    ArticleChangeComponent,
    ArticleSearchComponent,
    CategorySearchComponent,
    DateSearchComponent,
    AdminUsersComponent,
    AccountDetailComponent,
    AccountChangeComponent,
    AdminUserChangeComponent,
    MessagesComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
