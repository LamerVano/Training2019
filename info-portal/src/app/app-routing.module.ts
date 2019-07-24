import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoriesComponent } from './categories/categories.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';
import { ArticlesComponent } from './articles/articles.component';
import { ArticleDetailComponent } from './article-detail/article-detail.component';
import { ArticleAddComponent } from './article-add/article-add.component';
import { LogInComponent } from './log-in/log-in.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './_guard/authGuard';
import { CategoryAddComponent } from './category-add/category-add.component';
import { ArticleChangeComponent } from './article-change/article-change.component';
import { ArticleSearchComponent } from './article-search/article-search.component';
import { DateSearchComponent } from './date-search/date-search.component';
import { CategorySearchComponent } from './category-search/category-search.component';
import { AdminUsersComponent } from './admin-users/admin-users.component';
import { AccountService } from './services/account.service';
import { AdminUserChangeComponent } from './admin-user-change/admin-user-change.component';
import { AccountChangeComponent } from './account-change/account-change.component';
import { AccountDetailComponent } from './account-detail/account-detail.component';

const routes: Routes = [
  { path: '', redirectTo: 'articles', pathMatch: 'full' },
  { path: 'categories', component: CategoriesComponent },
  { path: 'articles/:id', component: ArticlesComponent },
  { path: 'articles', component: ArticlesComponent },
  { path: 'category/detail/:id', component: CategoryDetailComponent, canActivate: [AuthGuard] },
  { path: 'category/add', component: CategoryAddComponent, canActivate: [AuthGuard] },
  { path: 'article/detail/:id', component: ArticleDetailComponent },
  { path: 'article/change/:id', component: ArticleChangeComponent, canActivate: [AuthGuard] },
  { path: 'article/add', component: ArticleAddComponent, canActivate: [AuthGuard] },
  { path: 'search', component: ArticleSearchComponent },
  { path: 'searchByDate', component: DateSearchComponent },
  { path: 'searchByCategory', component: CategorySearchComponent },
  { path: 'login', component: LogInComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'admin/users', component: AdminUsersComponent },
  { path: 'admin/user/:id', component: AdminUserChangeComponent },
  { path: 'account', component: AccountDetailComponent },
  { path: 'account/change', component: AccountChangeComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }


