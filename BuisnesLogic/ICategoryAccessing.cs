﻿using System.Collections.Generic;

using Common;

namespace BuisnesLogic
{
    public interface ICategoryAccessing: IService<Category>
    {
        IEnumerable<Category> Search(string term);
    }
}
