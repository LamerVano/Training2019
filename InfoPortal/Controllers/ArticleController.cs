using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Common;
using BuisnesLogic;
using Unity;

namespace InfoPortal.Controllers
{
    public class ArticleController : ApiController
    {
        [Dependency]
        IAccessing Accessing { get; set; }

        // GET api/article/5
        public IArticle Get(int id)
        {
            return Accessing.GetArticle(id);
        }
        
        // PUT api/article/5
        public void Put(int id, [FromBody]IArticle article)
        {
            Accessing.ChangeArticle(article);
        }

        // DELETE api/article/5
        public void Delete(int id)
        {
            Accessing.DelArticle(id);
        }
    }
}
