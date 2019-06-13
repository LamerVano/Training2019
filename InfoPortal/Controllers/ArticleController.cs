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
        IArticleAccessing _accessing;

        public ArticleController(IArticleAccessing accessing)
        {
            _accessing = accessing;
        }

        // GET api/article/5
        public Article Get(int id)
        {
            return _accessing.GetArticle(id);
        }

        public bool Post([FromBody]Article article)
        {
            if (ModelState.IsValid)
            {
                return _accessing.AddArticle(article);
            }
            else
            {
                return false;
            }
        }

        // PUT api/article/5
        //[Authorize(Roles = Roles.EDITOR)]
        public bool Put(int id, [FromBody]Article article)
        {
            if (ModelState.IsValid)
            {
                return _accessing.UpdateArticle(article);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/article/5
        //[Authorize(Roles = Roles.EDITOR)]
        public bool Delete(int id)
        {
            return _accessing.DelArticle(id);
        }
    }
}
