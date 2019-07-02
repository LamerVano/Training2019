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
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        ICategoryAccessing _categoryAccessing;

        public CategoryController(ICategoryAccessing categoryAccessing)
        {
            _categoryAccessing = categoryAccessing;
        }


        [HttpGet]
        public IEnumerable<Category> GetCategories()
        {
            return _categoryAccessing.List();
        }
                
        [HttpPost]
        public bool AddCategory([FromBody]Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryAccessing.Add(category);

                return true;
            }
            else
            {
                return false;
            } 
        }

        [HttpPut]
        public bool EditCategory(int id, [FromBody]Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryAccessing.Edit(category);

                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpDelete]
        public bool DeleteCategory([FromBody]Category category)
        {
            _categoryAccessing.Delete(category);

            return true;
        }
    }
}
