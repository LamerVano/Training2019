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
        [Route("byCategory/{id:int}")]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IEnumerable<Article> GetArticlesOfCategory(int id)
        {
            return _accessing.GetByCategoryId(id);
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public Article GetArticle(int id)
        {
            return _accessing.GetById(id);
        }

        [HttpPost]
        public void AddArticle([FromBody]Article article, [FromBody] HttpPostedFileBase image)
        {
            Validation();


            string path = "~/Content/Articles/" + article.Id;

            string imgType = "." + image.FileName.Split('.')[1];

            article.Picture = path + imgType;

            try
            {
                image.SaveAs(article.Picture);
            }
            catch(NotImplementedException)
            {                
                throw new NotImplementedException("Not Modify because Image don't Save");
            }
            
            _accessing.Add(article);

        }

        [HttpPut]
        public void EditArticle(int id, [FromBody]Article article, [FromBody] HttpPostedFileBase image)
        {
            Validation();
            
            try
            {
                image.SaveAs(article.Picture);
            }
            catch (NotImplementedException)
            {
                throw new NotImplementedException("Not Save because Image don't Save");
            }

            _accessing.Add(article);

        }

        [HttpPut]
        public void EditArticle(int id, [FromBody]Article article)
        {
            Validation();

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
                        message += err.ErrorMessage + " ";
                }

                throw new NotValidException(message);
            }
        }
    }
}
