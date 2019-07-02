using System.Collections.Generic;
using Common;
using DataAcces;

namespace BuisnesLogic
{
    public class ArticleAccessing : IArticleAccessing
    {
        IArticleData _articleData;
        IArticleRefData _articleRefData;

        public ArticleAccessing(IArticleData articleData, IArticleRefData articleRefData)
        {
            _articleData = articleData;
            _articleRefData = articleRefData;
        }

        public void Add(Article entity)
        {
            _articleData.Add(entity);
            _articleRefData.Add(entity.References);
        }

        public void Delete(Article entity)
        {
            _articleData.Delete(entity);
            _articleRefData.Delete(entity.References);
        }

        public void Edit(Article entity)
        {
            _articleData.Edit(entity);
            _articleRefData.Edit(entity.References);
        }

        public IEnumerable<Article> GetByCategoryId(int categoryId)
        {
            List<Article> articles = new List<Article>();
            articles.AddRange(_articleData.GetByCategoryId(categoryId));

            foreach(Article article in articles)
            {
                article.References = _articleRefData.GetById(article.Id);
            }

            return articles;
        }

        public Article GetById(int id)
        {
            Article article = _articleData.GetById(id);

            article.References = _articleRefData.GetById(article.Id);

            return article;
        }

        public IEnumerable<Article> List()
        {
            List<Article> articles = new List<Article>();
            articles.AddRange(_articleData.List());

            foreach (Article article in articles)
            {
                article.References = _articleRefData.GetById(article.Id);
            }

            return articles;
        }
    }
}
