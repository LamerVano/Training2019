using System.Collections.Generic;
using Common;
using DataAcces;

namespace BuisnesLogic
{
    public class CategoryAccessing : ICategoryAccessing
    {
        ICategoryData _categoryData;
        ICategoryRefData _categoryRefData;
        
        public CategoryAccessing(ICategoryData categoryData, ICategoryRefData categoryRefData)
        {
            _categoryData = categoryData;
            _categoryRefData = categoryRefData;
        }

        public void Add(Category entity)
        {
            _categoryData.Add(entity);
            _categoryRefData.Add(entity.Articles);
        }

        public void Delete(Category entity)
        {
            _categoryData.Delete(entity);
            _categoryRefData.Delete(entity.Articles);
        }

        public void Edit(Category entity)
        {
            _categoryData.Edit(entity);
            _categoryRefData.Edit(entity.Articles);
        }

        public Category GetById(int id)
        {
            Category category = _categoryData.GetById(id);

            category.Articles = _categoryRefData.GetById(category.Id);

            return category;
        }

        public IEnumerable<Category> List()
        {
            List<Category> categories = new List<Category>();
            categories.AddRange(_categoryData.List());

            foreach (Category category in categories)
            {
                category.Articles = _categoryRefData.GetById(category.Id);
            }

            return categories;
        }
    }
}
