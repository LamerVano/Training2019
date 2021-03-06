﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace DataAcces
{
    public interface IArticleRefData: IRepository<ArticleReferences>
    {
        IEnumerable<ArticleReference> ListShortArticle();
    }
}
