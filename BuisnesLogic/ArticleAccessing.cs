using System.Collections.Generic;
using Common;
using DataAcces;

namespace BuisnesLogic
{
    public class ArticleAccessing : IArticleAccessing
    {
        IArticleData _articleData;

        public ArticleAccessing(IArticleData articleData)
        {
            _articleData = articleData;
        }

        public bool AddArticle(Article article)
        {
            return _articleData.AddArticle(article);
        }

        public bool UpdateArticle(Article article)
        {
            return _articleData.UpdateArticle(article);
        }

        public bool DelArticle(int articleId)
        {
            return _articleData.DelArticle(articleId);
        }

        public Article GetArticle(int articleId)
        {
            return _articleData.GetArticle(articleId);
        }
        
        public IEnumerable<Article> GetArticles()
        {
            return _articleData.GetArticles();
        }
    }
}
