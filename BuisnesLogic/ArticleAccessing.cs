using System;
using System.Collections.Generic;
using Common;
using DataAcces;

namespace BuisnesLogic
{
    public class ArticleAccessing : IArticleAccessing
    {
        IArticleData _articleData;
        IArticleRefData _articleRefData;
        ICategoryRefsData _categoryRefsData;

        public ArticleAccessing(IArticleData articleData, IArticleRefData articleRefData, ICategoryRefsData categoryRefsData)
        {
            _articleData = articleData;
            _articleRefData = articleRefData;
            _categoryRefsData = categoryRefsData;
        }

        public void Add(Article entity)
        {
            _articleData.Add(entity);

            entity.Id = _articleData.GetLastIndex();

            if (entity.ArticleRefs != null)
                _articleRefData.Add(entity.ArticleRefs);

            if (entity.CategoryRefs != null)
                _categoryRefsData.Add(entity.CategoryRefs);
        }

        public void Delete(int id)
        {
            _articleData.Delete(id);
            _articleRefData.Delete(id);
            _categoryRefsData.Delete(id);
        }

        public void Edit(Article entity)
        {
            _articleData.Edit(entity);
            _articleRefData.Edit(entity.ArticleRefs);
            _categoryRefsData.Edit(entity.CategoryRefs);
        }

        public IEnumerable<Article> GetByCategoryId(int categoryId)
        {
            List<Article> articles = new List<Article>();
            articles.AddRange(_articleData.GetByCategoryId(categoryId));

            foreach(Article article in articles)
            {
                article.ArticleRefs = _articleRefData.GetById(article.Id);
                article.CategoryRefs = _categoryRefsData.GetById(article.Id);
            }

            return articles;
        }

        public Article GetById(int id)
        {
            Article article = _articleData.GetById(id);

            article.ArticleRefs = _articleRefData.GetById(article.Id);
            article.CategoryRefs = _categoryRefsData.GetById(article.Id);

            return article;
        }

        public IEnumerable<Article> List()
        {
            List<Article> articles = new List<Article>();
            articles.AddRange(_articleData.List());

            foreach (Article article in articles)
            {
                article.ArticleRefs = _articleRefData.GetById(article.Id);
                article.CategoryRefs = _categoryRefsData.GetById(article.Id);
            }

            return articles;
        }

        public IEnumerable<ArticleReference> ListShortArticle()
        {
            return _articleRefData.ListShortArticle();
        }

        public IEnumerable<Article> GetByUserId(string id)
        {
            List<Article> articles = new List<Article>();
            articles.AddRange(_articleData.List());

            return articles.FindAll(article => article.UserId == id);
        }

        public IEnumerable<Article> Search(string term)
        {
            List<Article> articles = new List<Article>();
            articles.AddRange(_articleData.List());
            
            return articles.FindAll(article => article.Name.Contains(term));
        }

        public IEnumerable<Article> SearchByDate(DateTime date)
        {
            List<Article> articles = new List<Article>();
            articles.AddRange(_articleData.List());

            return articles.FindAll(article => article.Date == date);
        }

        public int GetLastIndex()
        {
            return _articleData.GetLastIndex();
        }
    }
}
