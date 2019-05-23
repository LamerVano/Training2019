using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAcces;
using Common;

namespace BuisnesLogic
{
    public class Accessing : IAccessing
    {
        public IDataAcces DataAccess { get; set; }

        public Accessing(IDataAcces dataAcces)
        {
            DataAccess = dataAcces;
        }

        public IEnumerable<ICategory> GetCategories()
        {
            return DataAccess.GetCategories();
        }

        public IEnumerable<IArticle> GetArticles(int categoryId)
        {
            return DataAccess.GetArticles(categoryId);
        }

        public IEnumerable<IArticle> GetArticles()
        {
            return DataAccess.GetArticles();
        }

        public IEnumerable<IUser> GetUsers()
        {
            return DataAccess.GetUsers();
        }



        public ICategory GetCategory(int categoryId)
        {
            return DataAccess.GetCategory(categoryId);
        }

        public IArticle GetArticle(int articleId)
        {
            return DataAccess.GetArticle(articleId);
        }

        public IUser GetUser(int userId)
        {
            return DataAccess.GetUser(userId);
        }



        public bool AddCategory(ICategory category)
        {
            return DataAccess.AddCategory(category);
        }

        public bool AddArticle(IArticle article)
        {
            return DataAccess.AddArticle(article);
        }

        public bool AddUser(IUser user)
        {
            return DataAccess.AddUser(user);
        }



        public bool DelCategory(int categoryId)
        {
            return DataAccess.DelCategory(categoryId);
        }

        public bool DelArticle(int articleId)
        {
            return DataAccess.DelCategory(articleId);
        }

        public bool DelUser(int userId)
        {
            return DataAccess.DelUser(userId);
        }



        public bool ChangeCategory(ICategory category)
        {
            return DataAccess.ChangeCategory(category);
        }

        public bool ChangeArticle(IArticle article)
        {
            return DataAccess.ChangeArticle(article);
        }

        public bool ChangeUser(IUser user)
        {
            return DataAccess.ChangeUser(user);
        }

    }
}
