using BuisnesLogic;
using Common;
using Common.Exceptions;
using InfoPortal.Flters;
using MyLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Unity;
using WebApi.OutputCache.V2;

namespace InfoPortal.Controllers
{
    [Authorize]
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        ICategoryAccessing _categoryAccessing;

        public CategoryController(ICategoryAccessing categoryAccessing)
        {
            _categoryAccessing = categoryAccessing;
        }


        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Category> GetCategories()
        {
            return _categoryAccessing.List();
        }
                
        [HttpPost]
        public void AddCategory([FromBody]Category category)
        {
            Validation();

            _categoryAccessing.Add(category);
        }

        [HttpPut]
        public void EditCategory([FromBody]Category category)
        {
            Validation();

            _categoryAccessing.Edit(category);
        }

        [HttpDelete]
        public void DeleteCategory(int id)
        {
            _categoryAccessing.Delete(id);
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Category> SearchCategory([FromUri]string name)
        {
            return _categoryAccessing.Search(name);
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
