using log4net;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http.Filters;

namespace WebAPI.Filter
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        ILog log = LogManager.GetLogger(typeof(ExceptionFilter));

        private HttpResponseMessage GetResponse(string code, string message)
        {
            var resultModel = new Result() { Code = code, Msg = message };

            return new HttpResponseMessage()
            {
                Content = new ObjectContent<Result>(
                    resultModel,
                    new JsonMediaTypeFormatter(),
                    "application/json"
                    )
            };
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response == null)
            {
                actionExecutedContext.Response = GetResponse("500", actionExecutedContext.Exception.Message);
            }
            string message = string.Format("//--------报错信息-------\r\n消息类型：{0}\r\n消息内容：{1}\r\n引发异常的方法：{2}\r\n引发异常源：{3}"
                    , actionExecutedContext.Exception.GetType().Name
                    , actionExecutedContext.Exception.Message
                     , actionExecutedContext.Exception.TargetSite
                     , actionExecutedContext.Exception.Source + actionExecutedContext.Exception.StackTrace
                     );
            //记录错误日志

            log.Error(message);
            base.OnException(actionExecutedContext);
        }
    }
}