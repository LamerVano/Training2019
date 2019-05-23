using BuisnesLogic;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Unity;

namespace InfoPortal.Controllers
{
    public class CategoryController : ApiController
    {
        [Dependency]
        IAccessing Accessing { get; set; }
        // GET api/values
        public IEnumerable<ICategory> Get()
        {
            return Accessing.GetCategories();
        }

        // GET api/values/5
        public IEnumerable<IArticle> Get(int id)
        {
            return Accessing.GetArticles(id);
        }

        // POST api/values
        public void Post([FromBody]ICategory category)
        {
            Accessing.AddCategory(category);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]ICategory category)
        {
            Accessing.ChangeCategory(category);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            Accessing.DelCategory(id);
        }
    }
}
