using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using UserCenter.OpenAPI.App_Start;

namespace UserCenter.OpenAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Api-v1",
                routeTemplate: "api/v1/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Api-v2",
                routeTemplate: "api/v2/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Api-v3",
                routeTemplate: "api/v3/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //添加版本路由选择
            config.Services.Replace(typeof(IHttpControllerSelector), new VersionControllerSelector(config));

            config.Filters.Add((AuthorizationFilter)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(AuthorizationFilter)));
        }
    }
}
