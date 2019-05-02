using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace WebAPI.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;
            if (!modelState.IsValid)
            {
                string error = string.Empty;
                var errorMessage = modelState.Values.SelectMany(m => m.Errors).Select(m => m.ErrorMessage).First();
                //foreach (var key in modelState.Keys)
                //{
                //    var state = modelState[key];
                //    if (state.Errors.Any())
                //    {
                //        error = key + ":" + state.Errors.First().ErrorMessage;
                //        if ( state.Errors.First().Exception!=null)
                //        {
                //            error+= "|" + state.Errors.First().Exception.Message;
                //        }
                //        break;
                //    }
                //}
                Result response = new Result() { Code = "500", Msg = errorMessage };
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Accepted)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(response), System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
                };
            }
        }
    }
}