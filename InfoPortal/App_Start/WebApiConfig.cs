using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Common;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Unity;
using InfoPortal.Flters;
using WebApi.OutputCache.V2;
using WebApi.OutputCache.Core.Cache;

namespace InfoPortal
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //var container = new UnityContainer();
            //container = DependencyInjection.DIContainer.UpdateContainer(container);
            //config.DependencyResolver = new Resolver.UnityResolver(container);

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.

            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr);

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();
                        
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "Category", id = RouteParameter.Optional }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Filters.Add(new Logging());
            config.Filters.Add(new SQLExeptionAtribute());
            config.Filters.Add(new NotValidExceptionAttribute());
            config.Filters.Add(new NotImplementedExceptionAtribute());

            config.CacheOutputConfiguration().RegisterCacheOutputProvider(() => new MemoryCacheDefault());
            config.CacheOutputConfiguration().RegisterDefaultCacheKeyGeneratorProvider(() => new DefaultCacheKeyGenerator());
        }
    }
}
