using System.Collections.Generic;

using Common;


namespace BuisnesLogic
{
    public interface IArticleAccessing: IService<Article>
    {
        IEnumerable<Article> GetByCategoryId(int categoryId);
    }
}
