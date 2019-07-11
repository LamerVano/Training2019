import { ArticleReferences } from './articleReferences';
import { CategoryReferences } from './categoryReferences';
export class Article {
  Id: number;
  UserId: number;
  Name: string;
  Date: Date;
  Language: string;
  Picture: string;
  Video: string;
  ArticleRefs: ArticleReferences;
  CategoryRefs: CategoryReferences;
}
