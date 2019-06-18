using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BLL;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Net;
using Model;
using Newtonsoft.Json;
using System.Text;

namespace WebAPI.Filter
{
    public class RequestAuthorizeAttribute : AuthorizeAttribute
    {
        //TB_UsersService us = new TB_UsersService();
        //重写基类的验证方式，加入我们自定义的Ticket验证
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //从http请求的头里面获取身份验证信息，验证是否是请求发起方的ticket
            var authorization = actionContext.Request.Headers.Authorization;
            if ((authorization != null) && (authorization.Parameter != null))
            {
                //解密用户ticket,并校验用户名密码是否匹配
                var encryptTicket = authorization.Parameter;
                //if (us.ValidateTicket(encryptTicket))
                //{
                //    base.IsAuthorized(actionContext);
                //}
                //else
                //{
                //    HandleUnauthorizedRequest(actionContext);
                //}
            }
            //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
            else
            {
                var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
                bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
                if (isAnonymous)
                {
                    base.OnAuthorization(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                };
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            var response = filterContext.Response = filterContext.Response ?? new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            Result obj = new Result() { Code = "401", Msg = "token失效,请重新登录!" };
            string str = JsonConvert.SerializeObject(obj);
            response.Content = new StringContent(str, Encoding.UTF8, "application/json");
        }
    }
}
