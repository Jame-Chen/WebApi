using System.Web.Http;
using WebActivatorEx;
using WebAPI;
using Swashbuckle.Application;
using WebAPI.Filter;

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
                        c.SingleApiVersion("v1", "ºó¶Ë½Ó¿ÚWebAPI");
                        c.IncludeXmlComments(GetXmlCommentsPath());
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.EnableApiKeySupport("Authorization", "header");
                    });
        }

        private static string GetXmlCommentsPath()
        {
            return string.Format("{0}/App_Data/WebAPI.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
