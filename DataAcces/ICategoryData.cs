using Common;
using System.Collections.Generic;

namespace DataAcces
{
    public interface ICategoryData
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Article> GetArticles(int categoryId);

        Category GetCategory(int categoryId);

        bool AddCategory(Category category);

        bool DelCategory(int categoryId);

        bool UpdateCategory(Category category);
    }
}
