using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace BuisnesLogic
{
    public interface IAccessing
    {
        IEnumerable<ICategory> GetCategories();
        IEnumerable<IArticle> GetArticles(int categoryId);
        IEnumerable<IArticle> GetArticles();
        IEnumerable<IUser> GetUsers();

        ICategory GetCategory(int categoryId);
        IArticle GetArticle(int articleId);
        IUser GetUser(int userId);

        bool AddCategory(ICategory category);
        bool AddArticle(IArticle article);
        bool AddUser(IUser user);

        bool DelCategory(int categoryId);
        bool DelArticle(int articleId);
        bool DelUser(int userId);

        bool ChangeCategory(ICategory category);
        bool ChangeArticle(IArticle article);
        bool ChangeUser(IUser user);
    }
}
