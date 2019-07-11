using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Common;
using BuisnesLogic;
using Unity;
using System.Web;
using WebApi.OutputCache.V2;
using MyLogger;
using InfoPortal.Flters;
using Common.Exceptions;

namespace InfoPortal.Controllers
{
    [RoutePrefix("api/Article")]
    public class ArticleController : ApiController
    {
        IArticleAccessing _accessing;

        public ArticleController(IArticleAccessing accessing)
        {
            _accessing = accessing;
        }

        [HttpGet]
        public IEnumerable<ArticleReference> GetShortArticles()
        {
            return _accessing.ListShortArticle();
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<Article> GetAllArticles()
        {
            return _accessing.List();
        }

        [HttpGet]
        [Route("byCategory/{id:int}")]
        [CacheOutput(ClientTimeSpan = 1, ServerTimeSpan = 1)]
        public IEnumerable<Article> GetArticlesOfCategory(int id)
        {
            return _accessing.GetByCategoryId(id);
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 1, ServerTimeSpan = 1)]
        public Article GetArticle(int id)
        {
            return _accessing.GetById(id);
        }

        [HttpPost]
        public void AddArticle([FromBody]Article article)
        {
            article.Date = DateTime.Now;

            article.UserId = 1;

            try
            {
                Validation();
            }
            catch { }

            _accessing.Add(article);

        }

        [HttpPost]
        [Route("image")]
        public void AddArticle([FromBody]Article article, [FromBody] HttpPostedFileBase image)
        {
            article.Date = DateTime.Now;
            try
            {
                Validation();
            }
            catch { }


            string path = "~/Content/Articles/" + article.Id;

            string imgType = "." + image.FileName.Split('.')[1];

            article.Picture = path + imgType;

            try
            {
                image.SaveAs(article.Picture);
            }
            catch (NotImplementedException)
            {
                throw new NotImplementedException("Not Modify because Image don't Save");
            }

            _accessing.Add(article);

        }

        [HttpPut]
        [Route("image")]
        public void EditArticle([FromBody]Article article, [FromBody] HttpPostedFileBase image)
        {
            article.Date = DateTime.Now;
            try
            {
                Validation();
            }
            catch { }

            try
            {
                image.SaveAs(article.Picture);
            }
            catch (NotImplementedException)
            {
                throw new NotImplementedException("Not Save because Image don't Save");
            }

            _accessing.Edit(article);

        }

        [HttpPut]
        public void EditArticle([FromBody]Article article)
        {
            article.Date = DateTime.Now;
            try
            {
                Validation();
            }
            catch { }

            _accessing.Edit(article);
        }

        [HttpDelete]
        public void DeleteArticle(int id)
        {
            _accessing.Delete(id);
        }

        private void Validation()
        {
            if (ModelState.IsValid)
            {
                string models = "";

                foreach (var model in ModelState.Keys)
                {
                    models += model + " ";
                }

                Log.Debug("action: " + ActionContext.Request.Method.Method + " " + ActionContext.ActionDescriptor.ActionName + " Data: ' " + models + " ' Valid ");
            }
            else
            {
                string message = "";

                foreach (var mess in ModelState.Values)
                {
                    foreach (var err in mess.Errors)
                        message += err.Exception.Message + " ";
                }

                throw new NotValidException(message);
            }
        }
    }
}
