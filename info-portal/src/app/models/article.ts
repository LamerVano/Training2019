import { ArticleReferences } from './articleReferences';
export class Article {
  Id: number;
  UserId: number;
  Name: string;
  Date: Date;
  Language: string;
  Picture: string;
  Video: string;
  References: ArticleReferences;
}
