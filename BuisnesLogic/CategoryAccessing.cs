using System.Collections.Generic;
using Common;
using DataAcces;

namespace BuisnesLogic
{
    public class CategoryAccessing : ICategoryAccessing
    {
        ICategoryData _categoryData;
        
        public CategoryAccessing(ICategoryData categoryData)
        {
            _categoryData = categoryData;
        }

        public bool AddCategory(Category category)
        {
            return _categoryData.AddCategory(category);
        }

        public bool UpdateCategory(Category category)
        {
            return _categoryData.UpdateCategory(category);
        }

        public bool DelCategory(int categoryId)
        {
            return _categoryData.DelCategory(categoryId);
        }

        public IEnumerable<Article> GetArticles(int categoryId)
        {
            return _categoryData.GetArticles(categoryId);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categoryData.GetCategories();
        }

        public Category GetCategory(int categoryId)
        {
            return _categoryData.GetCategory(categoryId);
        }
    }
}
