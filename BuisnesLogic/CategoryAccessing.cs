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

        public void Add(Category entity)
        {
            _categoryData.Add(entity);
        }

        public void Delete(int id)
        {
            _categoryData.Delete(id);
        }

        public void Edit(Category entity)
        {
            _categoryData.Edit(entity);
        }

        public Category GetById(int id)
        {
            return _categoryData.GetById(id);
        }

        public IEnumerable<Category> List()
        {
            return _categoryData.List();
        }
    }
}
