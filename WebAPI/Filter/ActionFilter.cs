using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPI.Filter
{
    public class ActionFilter : ActionFilterAttribute
    {
        ILog log = LogManager.GetLogger(typeof(ActionFilter));
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var test = actionContext.ActionArguments;
            string url = actionContext.Request.RequestUri.ToString();
            string postStr = "";
            postStr += "\r\n请求url：" + url + "\r\n";
            postStr += "请求参数/值：\r\n";
            postStr += "{";
            foreach (var b in test)
            {
                Type t = b.Value.GetType();
                string name = t.Name;
                if (t.IsPrimitive || name.ToLower() == "string")
                {
                    postStr += b.Key + ":" + b.Value + ",";
                }
                else

                {
                    var typeArr = t.GetProperties();
                    foreach (var item in typeArr)
                    {
                        string key = item.Name;
                        string value = item.GetValue(b.Value).ToString();
                        postStr += key + ":" + value + ",";
                    }
                }
            }
            postStr = postStr.Substring(0, postStr.Length - 1);
            postStr += "}";
            postStr += "\r\n";
            log.Info(postStr);
        }
    }
}