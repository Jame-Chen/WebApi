using System.Web.Http;
using WebActivatorEx;
using WebAPI;
using Swashbuckle.Application;
using WebAPI.Filter;
using System.Web.Http.Cors;
using System;
using WebAPI.App_Start;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.OperationFilter<SwaggerFileUploadFilter>();
                    c.SingleApiVersion("v1", "WebApi�ӿ�");
                    c.IncludeXmlComments(GetXmlCommentsPath());
                    c.CustomProvider((defaultProvider) => new CachingSwaggerProvider(defaultProvider));

                })
                .EnableSwaggerUi(c =>
                {
                    //·��������Ŀ�����ռ�.�ļ�������.js�ļ�����
                    c.InjectJavaScript(thisAssembly, "WebAPI.Scripts.Swaggerui.swagger_lang.js");
                });
        }

        private static string GetXmlCommentsPath()
        {
            return string.Format("{0}/App_Data/WebAPI.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }

    }
}
