using BuisnesLogic;
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
           

            container.RegisterType<IAccessing, Accessing>();

            container.RegisterType<IDataAcces, DataAcces.DataAcces>();

            return container;
        }
    }
}
