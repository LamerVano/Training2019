using System.Collections.Generic;

using Common;

namespace BuisnesLogic
{
    public interface ICategoryAccessing
    {
        IEnumerable<Article> GetArticles(int categoryId);
        IEnumerable<Category> GetCategories();

        Category GetCategory(int categoryId);

        bool AddCategory(Category category);

        bool DelCategory(int categoryId);

        bool UpdateCategory(Category category);
    }
}
