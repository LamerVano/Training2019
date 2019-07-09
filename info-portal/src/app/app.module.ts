import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { ArticlesComponent } from './articles/articles.component';
import { CategoriesComponent } from './categories/categories.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';
import { AppRoutingModule } from './app-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ArticleDetailComponent } from './article-detail/article-detail.component';
import { ArticleAddComponent } from './article-add/article-add.component';
import { ArticleRefsComponent } from './article-refs/article-refs.component';

@NgModule({
  declarations: [
    AppComponent,
    ArticlesComponent,
    CategoriesComponent,
    CategoryDetailComponent,
    DashboardComponent,
    ArticleDetailComponent,
    ArticleAddComponent,
    ArticleRefsComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
