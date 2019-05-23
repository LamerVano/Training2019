using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Common;
using BuisnesLogic;
using DependencyInjection;
using Unity;

namespace InfoPortal.Controllers
{
    public class MainController : ApiController
    {
        IAccessing Accessing { get; set; }

        public MainController(IAccessing accessing)
        {
            Accessing = accessing;
        }

        // GET api/values
        public IEnumerable<IArticle> Get()
        {
            var articles = Accessing.GetArticles();
            return articles;
        }

        // GET api/values/5
        public IEnumerable<IArticle> Get(int id)
        {
            return Accessing.GetArticles(id);
        }

        // POST api/values
        public void Post([FromBody]IArticle article)
        {
            Accessing.AddArticle(article);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]IArticle article)
        {
            Accessing.ChangeArticle(article);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            Accessing.DelArticle(id);
        }
    }
}
