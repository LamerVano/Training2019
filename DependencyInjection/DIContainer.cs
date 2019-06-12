﻿using BuisnesLogic;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using DataAcces;

namespace DependencyInjection
{
    public class DIContainer
    {

        public static UnityContainer UpdateContainer(UnityContainer container)
        {
           

            container.RegisterType<IUserAccessing, UserAccessing>();
            container.RegisterType<IArticleAccessing, ArticleAccessing>();
            container.RegisterType<ICategoryAccessing, CategoryAccessing>();

            container.RegisterType<IUsersData, UserData>();
            container.RegisterType<IArticleData, ArticleData>();
            container.RegisterType<ICategoryData, CategoryData>();

            return container;
        }
    }
}
