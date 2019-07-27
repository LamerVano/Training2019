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
    [Authorize]
    [RoutePrefix("api/Article")]
    [AutoInvalidateCacheOutput(TryMatchType = true)]
    public class ArticleController : ApiController
    {
        IArticleAccessing _accessing;
        static bool _mustRevaliid = true;

        public ArticleController(IArticleAccessing accessing)
        {
            _accessing = accessing;
        }

        [HttpGet]
        [AllowAnonymous]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 10)]
        public IEnumerable<ArticleReference> GetShortArticles()
        {
            return _accessing.ListShortArticle();
        }

        [HttpGet]
        [Route("all")]
        [AllowAnonymous]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 10)]
        public IEnumerable<Article> GetAllArticles()
        {
            return _accessing.List();
        }

        [HttpGet]
        [Route("byCategory/{id:int}")]
        [AllowAnonymous]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 10)]
        public IEnumerable<Article> GetArticlesOfCategory(int id)
        {
            return _accessing.GetByCategoryId(id);
        }

        [HttpGet]
        [Route("byUser/{id}")]
        [AllowAnonymous]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 10)]
        public IEnumerable<Article> GetArticlesOfUser(string id)
        {
            return _accessing.GetByUserId(id);
        }

        [HttpGet]
        [AllowAnonymous]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 10)]
        public Article GetArticle(int id)
        {
            return _accessing.GetById(id);
        }

        [HttpPost]
        [CacheOutput(MustRevalidate = true)]
        public void AddArticle([FromBody]Article article)
        {
            article.Date = DateTime.Now;

            Validation();

            _accessing.Add(article);

        }

        [HttpPost]
        [CacheOutput(MustRevalidate = true)]
        [Route("image")]
        public void AddArticle()
        {
            var request = HttpContext.Current.Request;

            var image = request.Files["image"];

            Validation();

            int id = _accessing.GetLastIndex();

            string path = "~/Content/Article/" + id;

            string pathInModel = "/Content/Article/" + id;

            string imgType = "." + image.FileName.Split('.').Last();

            Article article = _accessing.GetById(id);

            article.Picture = pathInModel + imgType;

            try
            {
                image.SaveAs(path + imgType);
            }
            catch (NotImplementedException)
            {
                throw new NotImplementedException("Not Modify because Image don't Save");
            }

            _accessing.Edit(article);

        }

        [HttpPut]
        [CacheOutput(MustRevalidate = true)]
        [Route("image/{id:int}")]
        public void EditArticle(int id)
        {

            var request = HttpContext.Current.Request;

            var image = request.Files["image"];
            
            Article article = _accessing.GetById(id);

            string path = HttpContext.Current.Server.MapPath("~/Content/Article/" + id);

            string pathInModel = "/Content/Article/" + id;

            string imgType = "." + image.FileName.Split('.').Last();

            article.Picture = pathInModel + imgType;

            try
            {
                image.SaveAs(path + imgType);
            }
            catch (NotImplementedException)
            {
                throw new NotImplementedException("Not Save because Image don't Save");
            }

            _accessing.Edit(article);

        }

        [HttpPut]
        [CacheOutput(MustRevalidate = true)]
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
        [CacheOutput(MustRevalidate = true)]
        public void DeleteArticle(int id)
        {
            _accessing.Delete(id);
        }

        [HttpGet]
        [AllowAnonymous]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 10)]
        public IEnumerable<Article> SearchArticle([FromUri]string name)
        {
            return _accessing.Search(name);
        }

        [HttpGet]
        [AllowAnonymous]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 10)]
        //[Route("SearchByDate")]
        public IEnumerable<Article> SearchArticleByDate([FromUri]string date)
        {
            DateTime time = DateTime.Parse(date);
            return _accessing.SearchByDate(time);
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
