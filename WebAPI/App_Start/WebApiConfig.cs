﻿using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Routing;
using WebAPI.Filter;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //跨域配置
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            config.Filters.Add(new ModelValidationAttribute());
            //config.Filters.Add(new RequestAuthorizeAttribute());
            config.Filters.Add(new ExceptionFilter());
            config.Filters.Add(new ActionFilter());
            // Web API routes
            config.MapHttpAttributeRoutes();

            RouteTable.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            ).RouteHandler = new SessionControllerRouteHandler();//启用session
            // 干掉XML序列化器
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // 解决json序列化时的循环引用问题
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            // 对 JSON 数据使用混合大小写。驼峰式,但是是javascript 首字母小写形式.
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new  CamelCasePropertyNamesContractResolver();
            // 对 JSON 数据使用混合大小写。跟属性名同样的大小.输出
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(
               new Newtonsoft.Json.Converters.IsoDateTimeConverter()
               {
                   DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
               });
        }
    }
}
