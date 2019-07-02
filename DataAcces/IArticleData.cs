using Common;
using System.Collections.Generic;

namespace DataAcces
{
    public interface IArticleData: IRepository<Article>
    {
        IEnumerable<Article> GetByCategoryId(int categoryId);
    }
}
