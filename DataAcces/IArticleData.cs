using Common;
using System.Collections.Generic;

namespace DataAcces
{
    public interface IArticleData
    {
        IEnumerable<Article> GetArticles();

        Article GetArticle(int articleId);

        bool AddArticle(Article article);

        bool DelArticle(int articleId);

        bool UpdateArticle(Article article);
    }
}
