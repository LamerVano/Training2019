using System.Collections.Generic;

using Common;


namespace BuisnesLogic
{
    public interface IArticleAccessing
    {
        IEnumerable<Article> GetArticles();

        Article GetArticle(int articleId);

        bool AddArticle(Article article);

        bool DelArticle(int articleId);

        bool UpdateArticle(Article article);
    }
}
