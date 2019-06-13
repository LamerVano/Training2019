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
        ICategoryAccessing _accessing;

        public CategoryController(ICategoryAccessing accessing)
        {
            _accessing = accessing;
        }


        // GET api/values
        public IEnumerable<Category> Get()
        {
            return _accessing.GetCategories();
        }

        // GET api/values/5
        public IEnumerable<Article> Get(int id)
        {
            return _accessing.GetArticles(id);
        }

        // POST api/values
        //[Authorize(Roles = Roles.EDITOR)]
        public bool Post([FromBody]Category category)
        {
            if (ModelState.IsValid)
            {
                return _accessing.AddCategory(category);
            }
            else
            {
                return false;
            } 
        }

        // PUT api/values/5
        //[Authorize(Roles = Roles.EDITOR)]
        public bool Put(int id, [FromBody]Category category)
        {
            if (ModelState.IsValid)
            {
                return _accessing.UpdateCategory(category);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/values/5
        //[Authorize(Roles = Roles.EDITOR)]
        public bool Delete(int id)
        {
            return _accessing.DelCategory(id);
        }
    }
}
