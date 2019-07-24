using System;
using System.Collections.Generic;

using Common;


namespace BuisnesLogic
{
    public interface IArticleAccessing : IService<Article>
    {
        IEnumerable<Article> GetByCategoryId(int categoryId);
        IEnumerable<Article> GetByUserId(string id);
        IEnumerable<ArticleReference> ListShortArticle();
        IEnumerable<Article> Search(string term);
        IEnumerable<Article> SearchByDate(DateTime date);
        int GetLastIndex();
    }
}
